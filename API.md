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
    
    Response
    200 OK
    User

</details>

<details>
    <summary><code>POST /company/name</code></summary>
    Request
    {
        companyName: string 
    }

    Response
    200 OK

</details>

### Gamedata

<details>
    <summary><code>GET /gamedata/resources</code></summary>
    
    Response
    200 OK
    List of resources

</details>

<details>
    <summary><code>GET /gamedata/production-buildings</code></summary>
    
    Response
    200 OK
    List of production buildings

</details>

### Storage

<details>
    <summary><code>GET /storage</code></summary>
    
    Response
    200 OK
    List of storage resources

</details>

<details>
    <summary><code>POST /storage/buy</code></summary>
    Request
    {
        resourceId: number,
        quantity: number
    }

    Response
    200 OK

</details>

<details>
    <summary><code>POST /storage/sell</code></summary>
    Request
    {
        resourceId: number,
        quantity: number
    }

    Response
    200 OK

</details>

### Production

<details>
    <summary><code>GET /production</code></summary>
    
    Response
    200 OK
    List of production

</details>

<details>
    <summary><code>GET /production/{id}</code></summary>
    
    Response
    200 OK
    Production with that id

</details>

<details>
    <summary><code>POST /production/{id}</code></summary>
    Request 
    {
        processId: number,
        quantity: number
    }

    Response
    200 OK

</details>

<details>
    <summary><code>DELETE /production/{id}</code></summary>

    Response
    200 OK

</details>
