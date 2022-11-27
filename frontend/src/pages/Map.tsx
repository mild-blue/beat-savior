import React, { useEffect, useState } from "react";
import MapView, { Marker, PROVIDER_GOOGLE } from "react-native-maps";
import { StyleSheet } from "react-native";
import { HStack, Icon, Text, View, VStack } from "native-base";
import { getAedLocations } from "../services/aed/getAedLocations";
import { AedOutDto } from "../generated";
import Geolocation from "@react-native-community/geolocation";
import Ionicons from "react-native-vector-icons/Ionicons";

export const Map = () => {
  const [aedLocations, setAedLocations] = useState<AedOutDto[]>([]);
  const origin = { latitude: 50.0218938, longitude: 14.4626102 };

  useEffect(() => {
    getAedLocations().then(setAedLocations);
    Geolocation.getCurrentPosition(info => console.log(info));
  }, []);

  return (
    <View height={"100%"}>
      <View height={"92%"}>
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
          {aedLocations.map(aedLoc => (
            <Marker
              key={aedLoc.id}
              coordinate={{ latitude: aedLoc.latitude, longitude: aedLoc.longitude }}
              title={aedLoc.name}
              description={aedLoc.htmlDescription}>
              <Icon backgroundColor={"white"} rounded={10} as={Ionicons} name="medkit" color={"green.700"} size={6} />
            </Marker>
          ))}
        </MapView>
      </View>
      <VStack height={"100%"} backgroundColor={"white"}>
        <HStack h={"8%"} justifyContent={"center"} alignItems={"center"} space={3}>
          <Icon
            backgroundColor={"white"}
            rounded={10}
            as={Ionicons}
            name="notifications-off-outline"
            color={"green.700"}
            size={6}
          />
          <Text bold color={"green.700"}>
            No active alerts in your area
          </Text>
        </HStack>
      </VStack>
    </View>
  );
};
