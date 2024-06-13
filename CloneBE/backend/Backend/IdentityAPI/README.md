# FAMS - Identity Service

## References:

- [ ] [IdentityServer](https://docs.duendesoftware.com/identityserver/v7)


---


## Getting started

- IdentityService within a microservices architecture serves as a pivotal component in charge of managing authentication and authorization functionalities across various microservices.
- This service acts as a centralized component for user identity management, ensuring secure access control and seamless integration within the microservices.


---


## Overview - Identity Server (Duende)

- Authentication Server
- Implements OpenID Connect(OIDC) and OAuth 2.0
- Designed to be cusomisable solution
- NO longer open source (license required in production env)
- Single Sign-On solution

Reference: [Overview](https://docs.duendesoftware.com/identityserver/v7/overview/)


---


## OAuth 2.0

- A security standard giving one app permission to access data in another application
- Instead of using username, password. We solely need to provide a key that has app permission

## OpenID Connect (OIDC)

    - Add additional functionality around login and profile information about the person who is logged in
    - Developed by the OpenID Foundation, OIDC addresses the need for secure identity management in web application, offering a comprehensive solution for identity federation and single sign-on (SSO) - enabling user to authenticate once and access multiple applications without the need to re-enter credentials.

## How Duende Identity Server can help

    - Duende IdentityServer serves as middleware intergrating spec-compliant OpenID and OAuth 2.0 enpoints
    into any ASP.NET Core host.
    - Typically, you build (or re-use) an application that contains login and logout features and add the IdentityServer middleware. The middleware adds the necessary protocal heads to the application so that clients can talk to it using those standard protocals.
    - The hosting application can be as complex as you want. However, in this project we include authentication/federation related UI only.


---


## IdentityService Dependencies

- Base Package References: + Duende.IdentityServer.AspNetIdentity + Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore + Microsoft.AspNetCore.Identity.EntityFrameworkCore + Microsoft.AspNetCore.Identity.UI + Microsoft.EntityFrameworkCore.Tools + Microsoft.EntityFrameworkCore.SqlServer
  Those packages are used to initialize the foundation of IdentityServer.

- Additional Package References:
  - MassTransit.EntityFrameworkCore
  - MassTransit.RabbitMQ
  - Serilog.AspNetCore
  - ...


---


## Requesting a Token (Postman)

Reference: [TokenRequest](https://docs.duendesoftware.com/identityserver/v7/tokens/requesting/)

Machine to Machine communication

In this scenario, interactive user wants to call an Identity API to get access token

Prerequisites are: - define a client for the client credentials grant type - define an API Scope (optional) - grant the client access to the scope via the AllowedScopes property

- [ ] Request Format:

```
    [POST] [/connect/token]
    CONTENT-TYPE application/x-www-form-urlencoded

    client_id=client1
    client_secret=secret
    username=[username]
    password=[password]
    grant_type=password
    scope=scope1

```

- [ ] Expected response:

```

[HTTP/1.1] [200] [OK]

{

    "access_token": "2YotnFZFEjr1zCsicMWpAA...",
    "token_type": "bearer",
    "expires_in": 3600

}

```

You can config your own in [Config.cs], but I recommend available configuration for Postman and WebApp
[More Demo Client](https://demo.duendesoftware.com)

- [ ] Default Request Sample:

```

    [POST] [/connect/token]
    CONTENT-TYPE application/x-www-form-urlencoded

        client_id=postman
        client_secret=NotASecret
        username=bob
        password=Pass123$
        grant_type=password
        scope=FamsApp openid profile offline_access

```

- [ ] Response:

```

    {
        "access_token":"eyJhbGciOiJSUzI1NiIsImtpZCI6IkU4QjM2NUQ3RTY0ODg5NTU2M0VBMzkyNEMyRjk0OEFGIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJuYmYiOjE3MDk1MTQ4MzksImlhdCI6MTcwOTUxNDgzOSwiZXhwIjoxNzA5NTE4NDM5LCJzY29wZSI6WyJGYW1zQXBwIiwib3BlbmlkIiwicHJvZmlsZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXSwiY2xpZW50X2lkIjoicG9zdG1hbiIsInN1YiI6IjY4ZmI2NWUxLTI2ZjQtNDIwMy04N2IxLWEwODJlMzA2ZmVkZiIsImF1dGhfdGltZSI6MTcwOTUxNDgzOSwiaWRwIjoibG9jYWwiLCJ1c2VybmFtZSI6ImJvYiIsImp0aSI6IjIzNTI4Qzg1REYzMkNCMDlDNzdEQTlBMzBERjlBOTA4In0.q5M4Wlq5ksxJn02gV   hMgfT-yg7dBcks8mEVsajTpdhs-OcIbAXpoUvstwZB0U3tD3ZblWCofmFuXLlPQml0OMTzuI_ZmwbGXJSbWYr3VpftTFIiiNh37aXDynDqEUk9A-E6EnVi-yXt9alhzmcL9w84dfF1nnuup_b1YsqPP3ZB8zrEl9sQ8MDlDfYf4o7MwoDjEWIlghut9-1skkFouN8cIUBhOFxNw40W22bIt1nqgHa2VVO3eFzQi4BJXUsbKQvcxMOv2lJUvnsiZxlklz6MlBE4slnTrj5uTzSlWZLa8wBdExlnZduyz0y-1gVkHBls9tIgawNbXZjwWmZT6EQ",
        "expires_in": 3600,
        "token_type": "Bearer",
        "refresh_token": "619887ee-1d9f-4629-a0e0-1cda2156db5f",
        "scope": "FamsApp offline_access openid profile"
    }

```


---


## Requesting a Token (From FE)

- [ ] Default Request Sample:

```
    [POST] [/connect/token]  
    CONTENT-TYPE application/x-www-form-urlencoded

    client_id=webApp
    client_secret=FE_fams
    username=bob
    password=Pass123$
    grant_type=password
    scope=FamsApp openid profile offline_access

```


---


## Refresh Token (Postman)

```

    [POST] [/connect/token]  
    CONTENT-TYPE application/x-www-form-urlencoded

    client_id=postman
    client_secret=NotASecret
    username=bob
    grant_type=refresh_token
    scope=FamsApp openid profile offline_access
    refresh_token=[RefreshTokenId here...]

```

- [ ] Response:

```
{
    "id_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IkU4QjM2NUQ3RTY0ODg5NTU2M0VBMzkyNEMyRjk0OEFGIiwidHlwIjoiSldUIn0.eyJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJuYmYiOjE3MDk1MTUzMDAsImlhdCI6MTcwOTUxNTMwMCwiZXhwIjoxNzA5NTE1NjAwLCJhdWQiOiJwb3N0bWFuIiwiYXRfaGFzaCI6InBodU5jZ1lFNTl1T3pBUnlNTE5obHciLCJzdWIiOiJlNDVlMWY0ZS1hMTdjLTQ3NjUtYmE0OS0yOTE5MWU3M2EzMmEiLCJhdXRoX3RpbWUiOjE3MDk1MTUzMDAsImlkcCI6ImxvY2FsIn0.lrZBK3MNFgIzT7jM0yhiaYCfZxzddB-31iz6o_3Lz-Op87Rl9R5WlHf1GKXpUNFSKNQE6wgp0uAvTereaZEQDByIGLgdGUz8HZauxGevhLxA5TwGEdkqtOsgSf1FnEeSog89Vq8YDSZf7YUNldQLve72PINDJXWpWRH_GSzJ72Lf9Pf7TmUXvLQtTKHJQZdnjxRojqS-hthatGOyEYi4GDMM4u2pG5Qi7QRRkkiz_QQ7-znP4rjnyCv-cu82AscHc92n8mZn6tYWo4eZJieRmiN_JSpbE8bA44AcQcNCMhpdj7D3XWO6IjcKXDRPSSrSdiAbM0luhPR4kdFlEPB06w",

    "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IkU4QjM2NUQ3RTY0ODg5NTU2M0VBMzkyNEMyRjk0OEFGIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJuYmYiOjE3MDk1MTUzMDAsImlhdCI6MTcwOTUxNTMwMCwiZXhwIjoxNzA5NTE4OTAwLCJzY29wZSI6WyJwcm9maWxlIiwib3BlbmlkIiwiRmFtc0FwcCIsIm9mZmxpbmVfYWNjZXNzIl0sImF1dGhfdGltZSI6MTcwOTUxNTMwMCwic3ViIjoiN2IyOTVjZGYtNmRjYi00MGNiLTgxZTgtOWFlNGVlYjU2MThiIiwiaWRwIjoibG9jYWwiLCJ1c2VybmFtZSI6ImNsaWVudCIsImp0aSI6ImJlNGI2ZjcwLWJlMTgtNDUzMC04ODcyLTI4NGY0OTFiNDI3NiJ9.pV3NXZMH1wBaWdDdfwn7_lhuPeD0Gsr7kEOiOdRl9U_PeXayfJu7_xfYtFp5lCmtI8NEOCOdoPe0ptk-TcCZHj4a1A0lTzVDdzHE8kWTytrX1W05C57dktWAQuRn4UpGyD1lRwcUCVh1IYCeq3WEbTdJNdM7AMWfdfA-CiuReJbJ5mMM7B-ro6jA6WskDpA-GxBbU2I7qPNxX_GL-F_HRNYl7HvlM2LdPgbiN9t7YGuJMPbO82SAAWBypcqH3R_vrw_IJQm6yoCKAM1NpoJZNKyvU1z31AJ5r0it-RoSriG6tSgZzjiw4VpI-cZcbFtLxA3xJFb9EtNmBruaD3Qo6w",
    "expires_in": 3600,
    "token_type": "Bearer",
    "refresh_token": "bc58cc91-bbcf-46a1-abe0-8ec480e60a5a",
    "scope": "FamsApp openid profile offline_access"
}

```


---


## Refresh Token (From FE)

```

[POST] [/connect/token]  
CONTENT-TYPE application/x-www-form-urlencoded

    client_id=webApp
    client_secret=FE_fams
    username=bob
    grant_type=refresh_token
    scope=FamsApp openid profile offline_access
    refresh_token=[RefreshTokenId here...]

```

---


## Register

```

[POST][/api/identity]
CONTENT-TYPE: application/json

{

    "fullName": "John Doe",
    "email": "john.doe@example.com",
    "dob": "1990-05-15",
    "address": "123 Main St, City",
    "gender": "Male",
    "phone": "0767092033",
    "username": "johndoe",
    "password": "Pass123$",
    "roleId": null,
    "createdBy": null,
    "createdDate": "2024-02-01",
    "modifiedBy": null,
    "modifiedDate": null,
    "avatar": null,
    "status": true

}

```

- [ ] Response:

```

{

    "statusCode": 200,
    "message": "Register successfully",
    "isSuccess": true,
    "data": null,
    "errors": null

}

```


---


## Executing Directives

LOCAL DEVELOPMENT:

1. Open Command Line Interface (CLI) - VSCode Terminal, PowerShell...

2. Navigate to Project Directory

   - [cd][./Backend/IdentityAPI]

3. Execute with `dotnet run`, this command will build and run the project. Ensure that all necessary dependencies are installed
   - [dotnet run]

3.1. Execute with `dotnet watch` (Optional -for automatic rebuilds) - [dotnet watch]

---

DOCKER:

1. Open Command Line Interface (CLI) - VSCode Terminal, PowerShell...

2. Navigate to Root Project Directory

   - [cd][~/your.directory/fam_hcm24_cpl_net_02]

3. Build container

   - [docker-compose build][container-name]

4. Execute Docker Compose:

   - [docker-compose up] or [docker-compose up -d] with detached mode

5. Stop and remove containers:
   - [docker-compose down]
