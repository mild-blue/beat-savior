import { Button, Heading, HStack, Icon, Input, Pressable, Stack, Text, View, VStack } from "native-base";
import React, { useContext, useState } from "react";
import Ionicons from "react-native-vector-icons/Ionicons";
import { StackScreenProps } from "@react-navigation/stack";
import { ParamListBase } from "@react-navigation/native";
import { UserContext } from "../App";

export const Login = ({ navigation }: StackScreenProps<ParamListBase>) => {
  const [show, setShow] = useState(false);
  const { setUser } = useContext(UserContext);

  return (
    <View style={{ flex: 1, justifyContent: "center", alignItems: "center" }}>
      <VStack
        alignItems={"center"}
        space={4}
        w={{
          base: "75%",
          md: "25%",
        }}>
        <Heading>Login</Heading>
        <Stack space={4} w="100%" alignItems="center">
          <Input
            InputLeftElement={<Icon as={Ionicons} name="person-outline" size={5} ml="2" color="muted.400" />}
            placeholder="Name"
          />
          <Input
            type={show ? "text" : "password"}
            InputRightElement={
              <Pressable onPress={() => setShow(!show)}>
                <Icon name={show ? "eye-outline" : "eye-off-outline"} as={Ionicons} size={5} mr="2" color="muted.400" />
              </Pressable>
            }
            placeholder="Password"
          />
        </Stack>
        <HStack>
          <Button onPress={() => setUser(true)} flex={1} colorScheme={"red"}>
            Login
          </Button>
        </HStack>
        <VStack width={"100%"} space={2}>
          <Text textAlign={"center"}>No account yet? Just sign up.</Text>
          <HStack>
            <Button onPress={() => navigation.navigate("Register")} flex={1} colorScheme={"red"} variant={"outline"}>
              Sign up
            </Button>
          </HStack>
        </VStack>
      </VStack>
    </View>
  );
};
