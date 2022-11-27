import { AedApi, Configuration } from "../generated";

const apiConfiguration = new Configuration({
  baseOptions: { withCredentials: true },
});

const apiUrl = "https://bildmlue.azurewebsites.net/";

export const api = {
  aed: new AedApi(apiConfiguration, apiUrl),
};
