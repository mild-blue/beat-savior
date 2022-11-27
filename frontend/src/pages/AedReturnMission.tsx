import { Button, Divider, HStack, Icon, Text, VStack } from "native-base";
import Ionicons from "react-native-vector-icons/Ionicons";
import { SafeAreaView, StyleSheet } from "react-native";
import React, { useEffect, useState } from "react";
import MapView, { Marker, PROVIDER_GOOGLE } from "react-native-maps";
import MapViewDirections from "react-native-maps-directions";
import { showLocation } from "react-native-map-link";
import { StackScreenProps } from "@react-navigation/stack";
import { ParamListBase } from "@react-navigation/native";
import Geolocation, { GeolocationResponse } from "@react-native-community/geolocation";
import { AssignmentOutDto } from "../generated";

export const AedReturnMission = ({ route, navigation }: StackScreenProps<ParamListBase>) => {
  const missionData: AssignmentOutDto = route.params as AssignmentOutDto;
  const [currentPosition, setCurrentPosition] = useState<GeolocationResponse>();

  const origin = !currentPosition
    ? undefined
    : { latitude: currentPosition.coords.latitude, longitude: currentPosition.coords.longitude };
  const destination = {
    latitude: missionData.nearestAED.latitude,
    longitude: missionData.nearestAED.longitude,
  };
  const GOOGLE_MAPS_APIKEY = "";

  useEffect(() => {
    Geolocation.getCurrentPosition(info => setCurrentPosition(info));
  }, []);

  const sendToMaps = () => {
    showLocation({
      latitude: destination.latitude,
      longitude: destination.longitude,
      directionsMode: "walk", // optional, accepted values are 'car', 'walk', 'public-transport' or 'bike'
    });
  };

  if (!origin) {
    return null;
  }

  return (
    <SafeAreaView>
      <VStack height={"100%"} justifyContent={"space-between"}>
        <VStack flex={2}>
          <MapView
            provider={PROVIDER_GOOGLE} // remove if not using Google Maps
            style={{
              ...StyleSheet.absoluteFillObject,
            }}
            region={{
              latitude: origin.latitude,
              longitude: origin.longitude,
              latitudeDelta: 0.015,
              longitudeDelta: 0.0121,
            }}>
            <MapViewDirections
              origin={origin}
              destination={{ longitude: missionData.nearestAED.longitude, latitude: missionData.nearestAED.latitude }}
              apikey={GOOGLE_MAPS_APIKEY}
              strokeWidth={3}
              strokeColor="green"
            />
            <Marker
              coordinate={{
                latitude: origin.latitude,
                longitude: origin.longitude,
              }}
              title={"Your position"}>
              <Icon as={Ionicons} name="ellipse" color={"blue.600"} />
            </Marker>
            <Marker
              coordinate={{
                latitude: missionData.nearestAED.latitude,
                longitude: missionData.nearestAED.longitude,
              }}
              title={missionData.nearestAED.name}
              description={missionData.nearestAED.htmlDescription}>
              <Icon backgroundColor={"white"} as={Ionicons} name="medkit" color={"green.600"} />
            </Marker>
          </MapView>
        </VStack>
        <VStack justifyContent={"space-between"} space={3} mt={2} mx={4}>
          <HStack alignItems={"center"} justifyContent={"space-between"} space={3}>
            <VStack alignItems={"center"} justifyContent={"center"}>
              <Icon as={Ionicons} name="medkit-outline" color={"green.700"} size={20} />
            </VStack>
            <VStack maxWidth={200} space={1}>
              <Text noOfLines={2} lineHeight={15}>
                Proceed to the location and return AED
              </Text>
              <Text color={"gray.400"} lineHeight={15}>
                {missionData.nearestAED.name}
              </Text>
            </VStack>
            <VStack>
              <Text textAlign={"right"} fontSize={"md"} bold>
                378 m
              </Text>
            </VStack>
          </HStack>
          <HStack>
            <Button onPress={sendToMaps} width={"100%"} colorScheme={"red"}>
              Navigate in maps app
            </Button>
          </HStack>
          <Divider
            my="2"
            _light={{
              bg: "muted.300",
            }}
          />
          <HStack alignItems={"center"} space={3}>
            <Icon as={Ionicons} name="body-outline" color={"green.600"} size={"40px"} />
            <Text noOfLines={2} bold width={250}>
              After arriving to the location, return AED
            </Text>
            <Icon as={Ionicons} name="chevron-forward-outline" color={"red.700"} size={"40px"} />
          </HStack>
          <Divider
            my="2"
            _light={{
              bg: "muted.300",
            }}
          />
          <HStack space={4}>
            <Button flex={1} onPress={() => navigation.navigate("Home")} width={"100%"} colorScheme={"red"} mb={6}>
              Confirm returned AED
            </Button>
          </HStack>
        </VStack>
      </VStack>
    </SafeAreaView>
  );
};
