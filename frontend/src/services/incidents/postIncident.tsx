import { CreateIncidentInDto } from "../../generated";

export const postIncident = async (data: CreateIncidentInDto) => {
  const response = await fetch("https://bildmlue.azurewebsites.net/api/incidents/report", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
  return response.json();
};
