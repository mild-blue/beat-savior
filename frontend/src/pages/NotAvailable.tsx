import { Button, Icon, Text, VStack } from "native-base";
import React from "react";
import Ionicons from "react-native-vector-icons/Ionicons";
import { SafeAreaView } from "react-native";
import { StackScreenProps } from "@react-navigation/stack";
import { ParamListBase } from "@react-navigation/native";

export const NotAvailable = ({ navigation }: StackScreenProps<ParamListBase>) => {
  return (
    <SafeAreaView>
      <VStack height={"100%"} mx={8}>
        <VStack flex={4} justifyContent={"center"} alignItems={"center"}>
          <VStack flex={3} justifyContent={"center"} alignItems={"center"} space={5}>
            <Icon as={Ionicons} name="close-circle-outline" color={"red.700"} size={90} />
            <Text textAlign={"center"} fontSize={"3xl"} bold lineHeight={30}>
              Not available
            </Text>
            <Text textAlign={"center"} fontSize={"md"} lineHeight={20}>
              You answered that you are not available. Ambulance and other helpers are on their way to the incident.
              Thank you for your help!
            </Text>
          </VStack>
        </VStack>
        <VStack flex={2} justifyContent={"center"} alignItems={"center"}>
          <Button width={"100%"} onPress={() => navigation.navigate("Home")} colorScheme={"red"}>
            Home
          </Button>
        </VStack>
      </VStack>
    </SafeAreaView>
  );
};
