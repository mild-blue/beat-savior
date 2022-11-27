# Beat Savior
European Healthcare Hackathon 2022 submission.

![beatsavior-01](https://user-images.githubusercontent.com/40338867/204129431-81125d47-0dc8-486b-864c-821b315b62b1.png)

## Challenge - 4. Summon Help Now
Can you build an application that notifies nearby paramedics about a resuscitation emergency? The app would tell its user where AEDs are located and would help to save more lives by bringing all the resources to the emergency spot as fast as possible.

## Chek it out :eyes:
* Backend: https://bildmlue.azurewebsites.net/swagger/index.html
* Google Play: TODO
* Test flight: we can invite you 

## How to test
1. You can login with empty username
2. Click on Test Mode
3. Bellow are roles you can test

### Test CPR capable responder
1. Accept alert
2. Click on navigate maps app
3. Go back in the app
4. After the rescue click on End mission

### Test NOT CPR capable responder
1. Accept alert
2. Click on navigate maps app
3. Go back in the app
4. Confirm AED ppickup
5. Click on navigate maps app
6. Go back in the app
7. End mission and return AED
8. Click on navigate maps app
9. Go back in the app
10. Confirm return AED

### Other features
* signup as CPR capable person
* signup as CPR non-capable person (no training or certification)

## Technologies used

### Backed 
REST API with swagger documentation. We use .NET 7 with Postgre database with Entity Framework.
Backend is also capable of syncrhonizing data to FHIR.
We are suing FHIR implementation provided by [InterSystems](https://www.intersystems.com/cz)

### Frontend and mobile app
React Native

### Ohter
* Firely SDK
* Google maps
* Záchrnaka API for AEDs location

## Side note
The history has only one commit because, we had to hide our Google Maps API keys which we accidentely commited.
If you want full history, we can invite you into our private repo.

## Screenshots
![1](https://user-images.githubusercontent.com/13693312/204129302-64ba5a3a-ed05-4651-9bec-4930fb773372.png)
![2](https://user-images.githubusercontent.com/13693312/204129305-889c538a-77cd-4498-a817-31c5dc378b9b.PNG)
![3](https://user-images.githubusercontent.com/13693312/204129313-96eab58f-9796-4dab-8663-1f8017c938e9.PNG)
![4](https://user-images.githubusercontent.com/13693312/204129325-9f2b46e3-3faf-4eaa-9d49-fc34bbc18413.PNG)
