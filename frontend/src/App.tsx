import React, { createContext, useState } from "react";
import { Node } from "@babel/core";
import { Icon, NativeBaseProvider } from "native-base";
import { Map } from "./pages/Map";
import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { createStackNavigator } from "@react-navigation/stack";
import Ionicons from "react-native-vector-icons/Ionicons";
import { MissionAlert } from "./pages/MissionAlert";
import { CardiacArrestMission } from "./pages/CardiacArrestMission";
import { Test } from "./pages/Test";
import { Settings } from "./pages/Settings";
import { Metronome } from "./pages/Metronome";
import { Register } from "./pages/Register";
import { Login } from "./pages/Login";
import { NotAvailable } from "./pages/NotAvailable";
import { AedMission } from "./pages/AedMission";
import { AedReturnMission } from "./pages/AedReturnMission";

const Tab = createBottomTabNavigator();
const Stack = createStackNavigator();

export const UserContext = createContext({
  user: false,
  setUser: (arg: boolean) => {},
});

const App: () => Node = () => {
  const [user, setUser] = useState(false);

  return (
    <UserContext.Provider value={{ user, setUser }}>
      <NativeBaseProvider>
        <NavigationContainer>
          <Stack.Navigator>
            {(() => {
              if (!user) {
                return (
                  <>
                    <Stack.Screen name={"Login"} component={Login} options={{ headerShown: false }} />
                    <Stack.Screen name={"Register"} component={Register} options={{ headerShown: false }} />
                  </>
                );
              }
              return (
                <>
                  <Stack.Screen name={"Home"} component={HomeTabs} options={{ headerShown: false }} />
                  <Stack.Screen name={"Alert"} component={MissionAlert} options={{ headerShown: false }} />
                  <Stack.Screen name={"Mission"} component={CardiacArrestMission} options={{ headerShown: false }} />
                  <Stack.Screen name={"AedMission"} component={AedMission} options={{ headerShown: false }} />
                  <Stack.Screen
                    name={"AedReturnMission"}
                    component={AedReturnMission}
                    options={{ headerShown: false }}
                  />
                  <Stack.Screen name={"NotAvailable"} component={NotAvailable} options={{ headerShown: false }} />
                </>
              );
            })()}
          </Stack.Navigator>
        </NavigationContainer>
      </NativeBaseProvider>
    </UserContext.Provider>
  );
};

export default App;

const HomeTabs = () => {
  return (
    <Tab.Navigator>
      <Tab.Screen
        name="AED map"
        component={Map}
        options={{
          headerShown: false,
          tabBarIcon: ({ color, size }) => <Icon as={Ionicons} name="medkit-outline" color={color} size={size} />,
        }}
      />

      <Tab.Screen
        name="Test Mode"
        component={Test}
        options={{
          tabBarIcon: ({ color, size }) => <Icon as={Ionicons} name="hammer-outline" color={color} size={size} />,
        }}
      />

      <Tab.Screen
        name="Metronome"
        component={Metronome}
        options={{
          tabBarIcon: ({ color, size }) => <Icon as={Ionicons} name="alarm-outline" color={color} size={size} />,
        }}
      />

      <Tab.Screen
        name="Settings"
        component={Settings}
        options={{
          tabBarIcon: ({ color, size }) => <Icon as={Ionicons} name="settings-outline" color={color} size={size} />,
        }}
      />
    </Tab.Navigator>
  );
};
