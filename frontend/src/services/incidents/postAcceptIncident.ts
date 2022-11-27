import { AcceptIncidentInDto } from "../../generated";

export const postAcceptIncident = async (data: AcceptIncidentInDto, id: string) => {
  const response = await fetch(`https://bildmlue.azurewebsites.net/api/incidents/${id}/accept`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
  return response.json();
};
