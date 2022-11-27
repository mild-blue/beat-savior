import { Button, HStack, View } from "native-base";
import React, { useContext } from "react";
import { UserContext } from "../App";

export const Settings = () => {
  const { setUser } = useContext(UserContext);

  return (
    <View style={{ flex: 1, justifyContent: "space-between", alignItems: "center" }}>
      <HStack></HStack>
      <HStack mx={4} mb={4}>
        <Button onPress={() => setUser(false)} flex={1} colorScheme={"red"}>
          Logout
        </Button>
      </HStack>
    </View>
  );
};
