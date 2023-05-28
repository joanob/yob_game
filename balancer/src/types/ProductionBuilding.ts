export interface ProductionBuilding {
  id: number;
  name: string;
  buildCost: number;
  processes: ProductionProcess[];
}

export const isProductionBuilding = (o: any): boolean => {
  const object: ProductionBuilding = {
    id: 0,
    name: "",
    buildCost: 0,
    processes: [],
  };
  const objectKeys = Object.keys(object);

  if (objectKeys.length != Object.keys(o).length) {
    return false;
  }

  for (const key of Object.keys(o)) {
    if (!objectKeys.includes(key)) {
      return false;
    }
  }

  for (const process of o.processes) {
    if (!isProductionProcess(process)) {
      return false;
    }
  }

  return true;
};

export interface ProductionProcess {
  id: number;
  name: string;
  input: ProductionProcessResource[];
  miliseconds: number;
  output: ProductionProcessResource[];
}

const isProductionProcess = (o: any): boolean => {
  const object: ProductionProcess = {
    id: 0,
    name: "",
    input: [],
    miliseconds: 0,
    output: [],
  };
  const objectKeys = Object.keys(object);

  if (objectKeys.length != Object.keys(o).length) {
    return false;
  }

  for (const key of Object.keys(o)) {
    if (!objectKeys.includes(key)) {
      return false;
    }
  }

  for (const input of o.input) {
    if (!isProductionProcessResource(input)) {
      return false;
    }
  }

  for (const output of o.output) {
    if (!isProductionProcessResource(output)) {
      return false;
    }
  }

  return true;
};

export interface ProductionProcessResource {
  resourceId: number;
  quantity: number;
}

const isProductionProcessResource = (o: any): boolean => {
  const object: ProductionProcessResource = { resourceId: 0, quantity: 0 };
  const objectKeys = Object.keys(object);

  if (objectKeys.length != Object.keys(o).length) {
    return false;
  }

  for (const key of Object.keys(o)) {
    if (!objectKeys.includes(key)) {
      return false;
    }
  }

  return true;
};
