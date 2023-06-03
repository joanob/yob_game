export interface ProductionBuilding {
  id: number;
  name: string;
  buildCost: number;
  processes: ProductionProcess[];
}

export interface ProductionProcess {
  id: number;
  name: string;
  input: ProductionProcessResource[];
  miliseconds: number;
  output: ProductionProcessResource[];
}

export interface ProductionProcessResource {
  resourceId: number;
  quantity: number;
}
