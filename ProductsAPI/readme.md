# Setup:

* Create (local) SQL database called "ProductAPI"
* Populate Secrets.json with the following values: 
```
{ 
  "ConnectionStrings:DatabaseConnection": "<DB connection string>",
  "Authentication:Key": "<Randomly generated string>"
}
```
* Run all 3 sql scripts in ``ProductsAPI\Database\Migration``
* Run the project and interact with it either through swagger page or by sending requests to either ``https://localhost:7266`` or ``http://localhost:5050``
* To access endpoints other than ``/Auth/login`` you have to first send username and password to this endpoint to get a JWT token and reply and then copy it (or enter it through swagger) in `Authorization` in format ``Bearer <token>``

Default users:
| Username | Password |
| --- | --- |
| admin | 1234 |
| user | asd |


Api Endpoints:
| Method | Path | Required role |
| --- | --- | -- |
| POST | /Auth/create | admin |
| POST | /Auth/login | - |
| GET | /Product/products | user, admin|
| POST | /Product/products | admin |
| GET | /Product/products/{id} | user, admin |
| PUT | /Product/products/{id} | admin |
| DELETE | /Product/products/{id} | admin |

Any newly created user has only user role. To make a new admin you would have to change the role in database.
