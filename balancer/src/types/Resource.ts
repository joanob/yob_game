export interface Resource {
  id: number;
  name: string;
  price: number;
}

export const isResource = (o: any): boolean => {
  const object: Resource = { id: 0, name: "", price: 0 };
  const objectKeys = Object.keys(object);

  if (objectKeys.length != Object.keys(o).length) {
    console.error("Resource keys length doesn't match");
    return false;
  }

  for (const key of Object.keys(o)) {
    if (!objectKeys.includes(key)) {
      console.error("Gamedata keys don't match");
      return false;
    }
  }

  return true;
};
