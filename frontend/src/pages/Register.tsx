import { Button, Heading, HStack, Icon, Input, Pressable, Stack, Switch, Text, View, VStack } from "native-base";
import React, { useContext, useState } from "react";
import { UserContext } from "../App";
import Ionicons from "react-native-vector-icons/Ionicons";
import { Controller, useForm } from "react-hook-form";
import { postRegisterUser } from "../services/user/postRegisterUser";

export const Register = () => {
  const { setUser } = useContext(UserContext);
  const [show, setShow] = useState(false);
  const [cpr, setCpr] = useState(false);
  const { control, formState, getValues } = useForm({ mode: "onBlur" });

  const register = () => {
    postRegisterUser({
      email: getValues("email"),
      phoneNumber: getValues("password"),
      name: getValues("name"),
      isCprCapable: cpr,
    }).then(() => setUser(true));
  };

  return (
    <View style={{ flex: 1, justifyContent: "center", alignItems: "center" }}>
      <VStack
        alignItems={"center"}
        space={4}
        w={{
          base: "75%",
          md: "25%",
        }}>
        <Heading>Sign up</Heading>
        <Stack space={4} w="100%" alignItems="center">
          <Controller
            control={control}
            render={({ field }) => (
              <Input
                onBlur={field.onBlur}
                InputLeftElement={<Icon as={Ionicons} name="person-outline" size={5} ml="2" color="muted.400" />}
                placeholder="Name"
                onChangeText={val => field.onChange(val)}
                value={field.value}
              />
            )}
            name="name"
            rules={{ required: "Field is required", minLength: 1 }}
            defaultValue=""
          />
          <Controller
            control={control}
            render={({ field }) => (
              <Input
                onBlur={field.onBlur}
                InputLeftElement={<Icon as={Ionicons} name="mail-outline" size={5} ml="2" color="muted.400" />}
                placeholder="E-Mail"
                onChangeText={val => field.onChange(val)}
                value={field.value}
              />
            )}
            name="email"
            rules={{ required: "Field is required", minLength: 1 }}
            defaultValue=""
          />
          <Controller
            control={control}
            render={({ field }) => (
              <Input
                onBlur={field.onBlur}
                type={show ? "text" : "password"}
                InputRightElement={
                  <Pressable onPress={() => setShow(!show)}>
                    <Icon
                      name={show ? "eye-outline" : "eye-off-outline"}
                      as={Ionicons}
                      size={5}
                      mr="2"
                      color="muted.400"
                    />
                  </Pressable>
                }
                placeholder="Password"
                onChangeText={val => field.onChange(val)}
                value={field.value}
              />
            )}
            name="password"
            rules={{ required: "Field is required", minLength: 1 }}
            defaultValue=""
          />
          <HStack width={"100%"} justifyContent={"space-between"} alignItems={"center"}>
            <Text>I'm able to perform CPR</Text>
            <Switch size="md" colorScheme={"red"} onToggle={setCpr} value={cpr} />
          </HStack>
        </Stack>
        <HStack>
          <Button onPress={register} flex={1} colorScheme={"red"} isDisabled={!formState.isValid}>
            Sign up
          </Button>
        </HStack>
      </VStack>
    </View>
  );
};
