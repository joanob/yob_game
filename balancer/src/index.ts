import { readGamedata } from "./reader";
import { validate } from "./validator";

console.log(""); // Initial empty line

// Get game data
const gamedata = readGamedata();

// Validate gamedata
validate(gamedata);

console.log(""); // Final empty line
