import { Button, Heading, Text, View, VStack } from "native-base";
import React, { useState } from "react";
import { useElapsedTime } from "use-elapsed-time";

export const Metronome = () => {
  const [count, setCount] = useState(0);
  const [play, setPlay] = useState(false);
  const { reset } = useElapsedTime({ isPlaying: play, updateInterval: 60 / 100, onUpdate: () => setCount(count + 1) });

  return (
    <View style={{ flex: 1, justifyContent: "center", alignItems: "center" }} m={4}>
      <VStack width={"100%"} height={"100%"} justifyContent={"space-between"}>
        <VStack></VStack>
        <VStack alignItems={"center"}>
          <Heading fontSize={"8xl"}>{count}</Heading>
          <Text>BPM: 100</Text>
        </VStack>
        <VStack space={4}>
          <Button
            onPress={() => {
              reset();
              setCount(0);
            }}
            colorScheme={"red"}
            variant={"outline"}>
            Reset
          </Button>
          <Button
            onPress={() => {
              setPlay(!play);
            }}
            colorScheme={"red"}>
            {play ? "Stop" : "Start"}
          </Button>
        </VStack>
      </VStack>
    </View>
  );
};
