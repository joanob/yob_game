# Your Own Boss API Reference

All non anonymous requests require a X-Access-Token Cookie with the JWT

## Response Codes

- 200 OK: everything went fine
- 400 Bad Request: data sent is not correct
- 401 Unauthorized: route is not anonymous
- 404 Not Found: object requested was not found
- 500 Internal Server Error: something went wrong

## Users

<code>/user</code>

- Id: number
- Username: string

<details>
    <summary><code>POST /signup</code></summary>

    Anonymous

    Request
    {
        "username": string,
        "password": string
    }

    Response
    200 OK

</details>

<details>
    <summary><code>POST /login</code></summary>

    Anonymous

    Request
    {
        "username": string,
        "password": string
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
