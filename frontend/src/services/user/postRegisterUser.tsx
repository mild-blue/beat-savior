import { UserRegisterInDto } from "../../generated";

export const postRegisterUser = async (data: UserRegisterInDto) => {
  const response = await fetch("https://bildmlue.azurewebsites.net/api/users/register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
  return response.json();
};
