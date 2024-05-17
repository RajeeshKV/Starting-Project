POST - http://localhost:5093/api/createApplication
{
    "Id" : "sdqw-cs32-d232-sdfd",
  "programName": "MyProgram",
  "personalDetails": [
    {
      "fieldName": "FirstName",
      "isMandatory": true
    },
    {
      "fieldName": "LastName",
      "isMandatory": true
    },
    {
      "fieldName": "Email",
      "isMandatory": true
    },
    {
      "fieldName": "Phone",
      "isMandatory": false
    }
  ],
  "questions": [
    {
      "type": 2,
      "questionText": "Answer me",
      "DropdownChoices": [
        { "Choice" : "Yes" },
        { "Choice" : "No" }
        ]
    }
  ]
}

POST - http://localhost:5093/api/submitApplication

{
  "programId": "a7bf4c2d-ba5d-401f-93da-32e2894f90cf",
  "personalDetails": {
    "FirstName": "Rajeesh",
    "LastName": "KV",
    "Email": "rajeeshkva2z@gmail.com"
  },
  "questionAnswers": {
    "Answer me": "Yes"
  }
}

GET  -- http://localhost:5093/api/getApplication/a7bf4c2d-ba5d-401f-93da-32e2894f90cf

PUT  -- http://localhost:5093/api/editApplication/a7bf4c2d-ba5d-401f-93da-32e2894f90cf  -- NOT WORKING

