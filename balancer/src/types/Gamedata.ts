import { ProductionBuilding, isProductionBuilding } from "./ProductionBuilding";
import { Resource, isResource } from "./Resource";

export interface GameData {
  resources: Resource[];
  productionBuildings: ProductionBuilding[];
}

export const isGameData = (o: any): boolean => {
  const object: GameData = { resources: [], productionBuildings: [] };
  const objectKeys = Object.keys(object);

  if (objectKeys.length != Object.keys(o).length) {
    console.error("Gamedata keys length doesn't match");
    return false;
  }

  for (const key of Object.keys(o)) {
    if (!objectKeys.includes(key)) {
      console.error("Gamedata keys don't match");
      return false;
    }
  }

  for (const resource of o.resources) {
    if (!isResource(resource)) {
      return false;
    }
  }

  for (const productionBuilding of o.productionBuildings) {
    if (!isProductionBuilding(productionBuilding)) {
      return false;
    }
  }

  return true;
};
