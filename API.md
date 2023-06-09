# Your Own Boss API Reference

## Game data

Resources, production buildings, production processes and some other data doesn't change over time. All this data is contained in a JSON file.

Ids must be unique for that type of entity. Processes have their own ids to have independence on production buildings.

```
{
    "resources" : [
        {
            "id": number,
            "name": string,
            "price": number
        }
    ],
    "productionBuildings": [
        {
            "id": number,
            "name": string,
            "buildCost": number,
            "processes": [
                {
                    "id": number,
                    "name": string,
                    "input": [
                        {
                            "resourceId": number,
                            "quantity": number
                        }
                    ],
                    "miliseconds": number,
                    "output": [
                        {
                            "resourceId": number,
                            "quantity": number
                        }
                    ],
                }
            ]
        }
    ]
}
```

## HTTP

All non anonymous requests require a X-Access-Token Cookie with the JWT

### Response Codes

- 200 OK: everything went fine
- 400 Bad Request: data sent is not correct
- 401 Unauthorized: route is not anonymous
- 404 Not Found: object requested was not found
- 500 Internal Server Error: something went wrong

### Users

<code>/user</code>

- Id: number
- Username: string
- CompanyName: string
- CompanyMoney: number

<details>
    <summary><code>POST /signup</code></summary>

    Create user with empty company name, initial money, initial properties and initial storage

    Anonymous

    Request
    {
        username: string,
        password: string
    }

    Response
    200 OK

</details>

<details>
    <summary><code>POST /login</code></summary>

    Create session tokens and check all storage created

    Anonymous

    Request
    {
        username: string,
        password: string
    }

    Response
    200 OK
    User
    HTTP-Only Cookie X-Access-Token = JWT
    Cookie X-Session-Started = true

</details>

<details>
    <summary><code>GET /session</code></summary>
    
    Check session tokens are valid
    
    Response
    200 OK
    User

</details>

<details>
    <summary><code>PUT /company/name</code></summary>

    Update company name

    Request
    {
        companyName: string
    }

    Response
    200 OK

</details>

### Gamedata

<code>/gamedata</code>

<details>
    <summary><code>GET /resources</code></summary>
    
    Response
    200 OK
    List of resources

</details>

<details>
    <summary><code>GET /production-buildings</code></summary>
    
    Response
    200 OK
    List of production buildings

</details>

### Storage

<code>/storage</code>

- UserId: number
- ResourceId: number
- Quantity: number

<details>
    <summary><code>GET /</code></summary>

    Get all storage

    Response
    200 OK
    List of storage

</details>

<details>
    <summary><code>POST /buy</code></summary>

    Buy resources

    Request
    {
        resourceId: number,
        quantity: number
    }

    Response
    200 OK

</details>

<details>
    <summary><code>POST /sell</code></summary>

    Sell resources

    Request
    {
        resourceId: number,
        quantity: number
    }

    Response
    200 OK

</details>

### Properties

<code>/properties</code>

- UserId: number
- ProductionBuildingId: number

<details>
    <summary><code>GET /</code></summary>

    Get all properties

    Response
    200 OK
    List of properties owned

</details>

<details>
    <summary><code>POST /{id}</code></summary>

    Buy property

    Response
    200 OK

</details>

### Production

<code>/production</code>

- UserId: number
- ProductionBuildingId: number
- ProductionProcessId: number
- Quantity: number
- Start: number
- End: number

<details>
    <summary><code>GET /</code></summary>

    Get all productions

    Response
    200 OK
    List of production

</details>

<details>
    <summary><code>GET /{id}</code></summary>

    Get production

    Response
    200 OK
    Production with that id

</details>

<details>
    <summary><code>POST /{id}</code></summary>
    
    Start production
    
    Request 
    {
        processId: number,
        quantity: number
    }

    Response
    200 OK

</details>

<details>
    <summary><code>DELETE /{id}</code></summary>

    End production

    Response
    200 OK

</details>
