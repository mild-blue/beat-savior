using BildMlue.Application.DTO.Users;
using BildMlue.Application.Exceptions;
using BildMlue.Application.Interfaces;
using BildMlue.Domain.Entities;
using BildMlue.Infrastructure.FHIR.Extensions;
using FluentValidation;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace BildMlue.Infrastructure.FHIR;

public class UserService : IUserService
{
    private readonly FhirClient _client;
    private readonly IValidator<UserRegisterInDto> _registerValidator;
    private readonly IValidator<UserUpdateInDto> _updateValidator;

    public UserService(FhirClientFactory fhirClientFactory, IValidator<UserRegisterInDto> registerValidator,
        IValidator<UserUpdateInDto> updateValidator)
    {
        _registerValidator = registerValidator;
        _updateValidator = updateValidator;
        _client = fhirClientFactory.Client;
    }

    public async Task<List<UserOutDto>> GetAll()
    {
        var persons = (await _client.SearchAsync<Person>())
            .Entry
            .Select(x => x.Resource)
            .OfType<Person>()
            .ToList();

        return persons.ConvertAll(p => new UserOutDto
        {
            Name = HumanNames.ToString(p.Name),
            Email = p.Telecom?.FirstOrDefault(x => x.System == ContactPoint.ContactPointSystem.Email)?.Value ?? "",
            PhoneNumber =
                p.Telecom?.FirstOrDefault(x => x.System == ContactPoint.ContactPointSystem.Phone)?.Value ?? "",
            FirebaseToken = p.GetFireBaseToken(),
            IsCprCapable = p.GetIsCprCapable()
        });
    }

    public async Task<FirstResponder> Get(string email)
    {
        var person = await Find(email);
        if (person is null)
        {
            throw new NotFoundException<FirstResponder>(email);
        }

        return new FirstResponder
        {
            Email = email,
            IsCprCapable = person.GetIsCprCapable()
        };
    }

    public async Task<bool> Register(UserRegisterInDto data)
    {
        await _registerValidator.ValidateAndThrowAsync(data);

        if (await Exists(data.Email))
        {
            return false;
        }

        var person = new Person();
        person.Name.AddRange(HumanNames.FromString(data.Name));
        person.SetEmail(data.Email);
        person.SetPhone(data.PhoneNumber);
        person.SetIsCprCapable(data.IsCprCapable);

        await _client.CreateAsync(person);
        return true;
    }

    public async Task<bool> Update(string email, UserUpdateInDto data)
    {
        await _updateValidator.ValidateAndThrowAsync(data);
        var person = await Find(email);

        if (person is null)
        {
            return false;
        }

        person.Name.Clear();
        person.Name.AddRange(HumanNames.FromString(data.Name));
        person.SetPhone(data.PhoneNumber);
        person.SetFireBaseToken(data.FirebaseToken);
        person.SetIsCprCapable(data.IsCprCapable);

        await _client.UpdateAsync(person);
        return true;
    }

    public async Task<bool> Delete(string email)
    {
        var person = await Find(email);
        if (person is null)
        {
            return false;
        }

        await _client.DeleteAsync($"Person/{person.Id}");
        return true;
    }

    private async Task<Person?> Find(string email)
    {
        var persons = (await _client.SearchAsync<Person>(new SearchParams("email", email)))
            .Entry
            .Select(x => x.Resource)
            .OfType<Person>()
            .ToList();

        return persons.FirstOrDefault();
    }

    private async Task<bool> Exists(string email)
    {
        var person = await Find(email);
        return person is not null;
    }
}