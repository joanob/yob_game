import { readFileSync } from "fs";
import path from "path";
import { GameData, isGameData } from "./types/Gamedata";
import { exit } from "process";

/**
 * Read and json and validate it's type GameData
 */
export const readGamedata = (): GameData => {
  const filePath = path.resolve(__dirname, "../../gamedata.json");
  const rawData = readFileSync(filePath);
  const object = JSON.parse(rawData.toString());
  if (!isGameData(object)) {
    console.error("gamedata.json is not type GameData");
    exit();
  }
  return object as GameData;
};
