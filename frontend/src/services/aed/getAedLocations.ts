import { AedOutDto } from "../../generated";

export const getAedLocations = async (): Promise<AedOutDto[]> => {
  const response = await fetch("https://bildmlue.azurewebsites.net/api/aed");
  return response.json();
};
