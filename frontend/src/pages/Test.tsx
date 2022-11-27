import { Button, HStack, View, VStack } from "native-base";
import React from "react";
import { StackScreenProps } from "@react-navigation/stack";
import { ParamListBase } from "@react-navigation/native";
import { postIncident } from "../services/incidents/postIncident";
import { IncidentOutDto } from "../generated";

export interface IncidentData {
  id: string;
  isCprCapable: boolean;
}

export const Test = ({ navigation }: StackScreenProps<ParamListBase>) => {
  const sendIncident = (isCprCap: boolean) => {
    const data = {
      latitude: 50.018842473834596,
      longitude: 14.464295095521827,
      callerIsCprCapable: isCprCap,
    };
    postIncident(data).then((response: IncidentOutDto) =>
      navigation.navigate("Alert", { id: response.id, isCprCapable: isCprCap }),
    );
  };

  return (
    <View style={{ flex: 1, justifyContent: "center", alignItems: "center" }}>
      <VStack mx={4} height={"100%"} justifyContent={"center"} alignItems={"center"} space={4}>
        <HStack>
          <Button onPress={() => sendIncident(true)} flex={1} colorScheme={"red"}>
            Test CPR capable responder
          </Button>
        </HStack>
        <HStack>
          <Button onPress={() => sendIncident(false)} flex={1} colorScheme={"red"} variant={"outline"}>
            Test NOT CPR capable responder
          </Button>
        </HStack>
      </VStack>
    </View>
  );
};
