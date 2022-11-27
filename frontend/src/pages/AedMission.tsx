import { Button, Divider, HStack, Icon, Text, VStack } from "native-base";
import Ionicons from "react-native-vector-icons/Ionicons";
import { SafeAreaView, StyleSheet } from "react-native";
import React, { useEffect, useState } from "react";
import MapView, { Marker, PROVIDER_GOOGLE } from "react-native-maps";
import MapViewDirections from "react-native-maps-directions";
import { showLocation } from "react-native-map-link";
import { StackScreenProps } from "@react-navigation/stack";
import { ParamListBase } from "@react-navigation/native";
import moment from "moment";
import { useElapsedTime } from "use-elapsed-time";
import Geolocation, { GeolocationResponse } from "@react-native-community/geolocation";
import { AssignmentOutDto } from "../generated";

export const AedMission = ({ route, navigation }: StackScreenProps<ParamListBase>) => {
  const missionData: AssignmentOutDto = route.params as AssignmentOutDto;
  const [currentPosition, setCurrentPosition] = useState<GeolocationResponse>();
  const [aedPicked, setAedPicked] = useState<boolean>(false);

  const { elapsedTime } = useElapsedTime({ isPlaying: true, updateInterval: 1 });

  const origin = !currentPosition
    ? undefined
    : { latitude: currentPosition.coords.latitude, longitude: currentPosition.coords.longitude };
  const destination = {
    latitude: missionData.patientLocation.latitude,
    longitude: missionData.patientLocation.longitude,
  };
  const GOOGLE_MAPS_APIKEY = "";

  useEffect(() => {
    Geolocation.getCurrentPosition(info => setCurrentPosition(info));
  }, []);

  const sendToMapsAed = () => {
    showLocation({
      latitude: missionData.nearestAED.latitude,
      longitude: missionData.nearestAED.longitude,
      directionsMode: "walk", // optional, accepted values are 'car', 'walk', 'public-transport' or 'bike'
    });
  };

  const sendToMapsPatient = () => {
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
        <VStack>
          <Text textAlign={"center"} fontSize={"xl"} bold my={4}>
            Elapsed Time: {moment("2022-11-26 00:00:00").add({ s: elapsedTime }).format("HH:mm:ss")}
          </Text>
        </VStack>
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
            {!aedPicked && (
              <MapViewDirections
                origin={origin}
                destination={{ longitude: missionData.nearestAED.longitude, latitude: missionData.nearestAED.latitude }}
                apikey={GOOGLE_MAPS_APIKEY}
                strokeWidth={3}
                strokeColor="green"
              />
            )}
            <MapViewDirections
              origin={{
                longitude: aedPicked ? origin.longitude : missionData.nearestAED.longitude,
                latitude: aedPicked ? origin.latitude : missionData.nearestAED.latitude,
              }}
              destination={destination}
              apikey={GOOGLE_MAPS_APIKEY}
              strokeWidth={3}
              strokeColor={aedPicked ? "red" : "#faa3a370"}
            />
            <Marker
              coordinate={{
                latitude: origin.latitude,
                longitude: origin.longitude,
              }}
              title={"Your position"}>
              <Icon as={Ionicons} name="ellipse" color={"blue.600"} />
            </Marker>
            {!aedPicked && (
              <Marker
                coordinate={{
                  latitude: missionData.nearestAED.latitude,
                  longitude: missionData.nearestAED.longitude,
                }}
                title={missionData.nearestAED.name}
                description={missionData.nearestAED.htmlDescription}>
                <Icon backgroundColor={"white"} as={Ionicons} name="medkit" color={"green.600"} />
              </Marker>
            )}
            <Marker
              coordinate={{
                latitude: destination.latitude - 0.0002,
                longitude: destination.longitude,
              }}
              title={"Patients position"}>
              <Icon as={Ionicons} name="heart" color={aedPicked ? "red.700" : "#faa3a370"} size={5} />
            </Marker>
          </MapView>
        </VStack>
        <VStack justifyContent={"space-between"} space={3} mt={2} mx={4}>
          <HStack alignItems={"center"} justifyContent={"space-between"} space={3}>
            <VStack alignItems={"center"} justifyContent={"center"}>
              <Icon as={Ionicons} name="medkit-outline" color={"green.700"} size={20} />
            </VStack>
            <VStack maxWidth={200} space={1}>
              {aedPicked ? (
                <Text noOfLines={2} lineHeight={15}>
                  Proceed to the location and bring AED to patient
                </Text>
              ) : (
                <Text noOfLines={2} lineHeight={15}>
                  Proceed to the location and pickup AED
                </Text>
              )}
              {!aedPicked && (
                <Text color={"gray.400"} lineHeight={15}>
                  {missionData.nearestAED.name}
                </Text>
              )}
            </VStack>
            <VStack>
              <Text textAlign={"right"} fontSize={"md"} bold>
                378 m
              </Text>
            </VStack>
          </HStack>
          <HStack>
            <Button
              onPress={() => (aedPicked ? sendToMapsPatient() : sendToMapsAed())}
              width={"100%"}
              colorScheme={"red"}>
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
              {aedPicked
                ? "After arriving to the location, give AED to rescuers"
                : "After arriving to the location, get AED. Check pin description"}
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
            {aedPicked ? (
              <Button
                flex={1}
                onPress={() => navigation.navigate("AedReturnMission", missionData)}
                width={"100%"}
                colorScheme={"green"}
                mb={6}>
                End mission and return AED
              </Button>
            ) : (
              <Button flex={1} onPress={() => setAedPicked(true)} width={"100%"} colorScheme={"green"} mb={6}>
                Confirm AED pickup
              </Button>
            )}
          </HStack>
        </VStack>
      </VStack>
    </SafeAreaView>
  );
};
