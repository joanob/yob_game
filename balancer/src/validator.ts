/**
 * Validate gamedata.json is type safe and ids are unique
 */

import { isDataView } from "util/types";
import { GameData } from "./types/Gamedata";
import { ProductionBuilding } from "./types/ProductionBuilding";
import { Resource } from "./types/Resource";

export const validate = (gamedata: GameData) => {
  console.log("Validating JSON");

  validateResources(gamedata.resources);

  validateProductionBuildings(gamedata.productionBuildings);

  console.log("JSON validation ended");
};

/**
 * Validations:
 * - Ids are unique, positive numbers
 * - Names are not empty
 * - Prices are positive numbers
 */
const validateResources = (resources: Resource[]) => {
  console.log("Validating resources");
  const ids: number[] = [];
  for (const resource of resources) {
    // Validate id is positive and unique
    if (resource.id < 1) {
      console.error(
        "Id " +
          resource.id +
          " of resource " +
          resource.name +
          " is not positive"
      );
    }
    if (ids.includes(resource.id)) {
      console.error(
        "Id " +
          resource.id +
          " of resource " +
          resource.name +
          " is already in use"
      );
    }
    ids.push(resource.id);

    // Validate name is not empty
    if (resource.name === "") {
      console.error("Resource with id " + resource.id + " has empty name");
    }

    // Validate price is positive
    if (resource.price < 1) {
      console.error(
        "Price " +
          resource.price +
          " of resource " +
          resource.name +
          " is not positive"
      );
    }
  }
};

/**
 *
 */
const validateProductionBuildings = (prodBuildings: ProductionBuilding[]) => {
  console.log("Validating production buildings");
  const prodIds: number[] = [];
  for (const prodBuilding of prodBuildings) {
    // Validate ids are positive and unique
    if (prodBuilding.id < 1) {
      console.error(
        "Id " +
          prodBuilding.id +
          " of prod building " +
          prodBuilding.name +
          " is not positive"
      );
    }
    if (prodIds.includes(prodBuilding.id)) {
      console.error(
        "Id " +
          prodBuilding.id +
          " of prod building " +
          prodBuilding.name +
          " is already in use"
      );
    }
    prodIds.push(prodBuilding.id);

    // Validate name is not empty
    if (prodBuilding.name === "") {
      console.error(
        "Prod building with id " + prodBuilding.id + " has empty name"
      );
    }

    // Validate buildcost is positive
    if (prodBuilding.buildCost < 1) {
      console.error(
        "Buildcost " +
          prodBuilding.buildCost +
          " of prod building " +
          prodBuilding.name +
          " is not positive"
      );
    }

    // Validate process
    // TODO: create all process validation
  }
};
