# Your Own Boss
Your Own Boss is a game developed with as many technologies as I can learn. Starting with Angular and Go, I plan on using React, Qwik, React Native and even native Android for the frontend and .NET, Node and Laravel for the backend.

## The game
Your Own Boss is a idle game, there's no need to spend hours playing. It only requires a few minutes and game can progress for hours. A crucial factor for me is to be able to decide when I want to log in again and having something to do at that moment.

### Story
You've inherited your grandpa's farm. Produce goods and sell them to earn money. Invest that money in more advanced production and commercial buildigs to build an empire.

### How it works
Resources can be bought and sold at a fixed price in the market. Use those resources in the production buildings to create more advanced resources or sell them in the commercial buildings. 

Creating and selling goods in buildings requires time. If a farm produces an apple every 10 seconds and I want to produce 90 apples it will take 30 minutes. After that time I can collect goods and decide what's next.

## Game progress
Game progresses by being able to create and sell more advanced and expensive resources. What I've found in other games is that the game progresses really fast in the early stages and after some time is nearly impossible to make any progress. It makes the game boring in the long term because you end up doing always the same.

I want to balance the game to have a more smooth progress and be enjoyable at all times. I don't know how to do it yet so I'm going to test it during development.

## Game entities
To have the reference across all technologies I will have the resources and buildings list here with their main characteristics.

### Resources
| Name | Price |
|--|--|
| Electricity | 1 |
| Water | 2 |
| Seed | 4 |
| Apple | 5 | 

### Buildings
| Name | Build cost |
|--|--|
| Solar panel | 500 |
| Water pump | 500 |
| Farm | 50000 |

#### Solar panel
| Name | Uses | Time | Creates |
| -- | -- | -- | -- |
| Sunny day |  | 1s | 1x Electricity |

#### Water pump
| Name | Uses | Time | Creates |
| -- | -- | -- | -- |
| Water well | 2x Electricity | 3s | 2x Water |

#### Farm
| Name | Uses | Time | Creates |
| -- | -- | -- | -- |
| Apple tree | 1x Seed <br> 2x Water | 10s | 10x Apple |
 