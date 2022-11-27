import React from "react";
import { HStack, Icon, Text, VStack } from "native-base";
import { SafeAreaView } from "react-native";
import Ionicons from "react-native-vector-icons/Ionicons";
import { StackScreenProps } from "@react-navigation/stack";
import { ParamListBase } from "@react-navigation/native";
import { postAcceptIncident } from "../services/incidents/postAcceptIncident";
import { IncidentData } from "./Test";
import { AssignmentOutDto, AssignmentType } from "../generated";
import moment from "moment";

export const MissionAlert = ({ route, navigation }: StackScreenProps<ParamListBase>) => {
  const missionData: IncidentData = route.params as IncidentData;

  const acceptMission = async () => {
    postAcceptIncident(
      {
        email: "Asd@dfsfs.com",
        isCprCapable: missionData.isCprCapable,
      },
      missionData.id,
    ).then((response: AssignmentOutDto) => {
      console.log(response);
      switch (response.type) {
        case AssignmentType.Resuscitation:
          navigation.navigate("Mission", response);
          break;
        case AssignmentType.DefibrillatorPickup:
          navigation.navigate("AedMission", response);
          break;
      }
    });
  };

  return (
    <SafeAreaView>
      <VStack height={"100%"}>
        <VStack
          flex={4}
          backgroundColor={missionData.isCprCapable ? "red.800" : "green.700"}
          justifyContent={"center"}
          alignItems={"center"}>
          <VStack flex={3} justifyContent={"center"} alignItems={"center"} maxWidth={200} space={5}>
            <Icon as={Ionicons} name="warning-outline" color={"white"} size={90} />
            <Text textAlign={"center"} color={"white"} fontSize={"3xl"} bold lineHeight={30}>
              Suspected cardiac arrest nearby
            </Text>
          </VStack>
          <VStack flex={1}>
            <Text textAlign={"center"} color={"white"} fontSize={"md"} bold lineHeight={30}>
              Alert recieved: {moment().format("HH:mm:ss")}
            </Text>
          </VStack>
        </VStack>
        <VStack flex={2} justifyContent={"center"} alignItems={"center"} space={7}>
          <VStack>
            <Text textAlign={"center"} fontSize={"2xl"} bold>
              Are you available?
            </Text>
          </VStack>
          <HStack maxWidth={250}>
            <VStack flex={1} alignItems={"center"} justifyContent={"center"}>
              <Icon
                onPress={acceptMission}
                as={Ionicons}
                name="checkmark-circle-outline"
                color={"green.700"}
                size={70}
              />
              <Text textAlign={"center"} fontSize={"lg"} bold>
                Yes
              </Text>
            </VStack>
            <VStack flex={1} alignItems={"center"} justifyContent={"center"}>
              <Icon
                onPress={() => navigation.navigate("NotAvailable")}
                as={Ionicons}
                name="close-circle-outline"
                color={"red.800"}
                size={70}
              />
              <Text textAlign={"center"} fontSize={"lg"} bold>
                No
              </Text>
            </VStack>
          </HStack>
        </VStack>
      </VStack>
    </SafeAreaView>
  );
};
