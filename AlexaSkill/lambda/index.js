// This sample demonstrates handling intents from an Alexa skill using the Alexa Skills Kit SDK (v2).
// Please visit https://alexa.design/cookbook for additional examples on implementing slots, dialog management,
// session persistence, api calls, and more.
const Alexa = require('ask-sdk-core');
const persistenceAdapter = require('ask-sdk-s3-persistence-adapter');
var https = require('https');
var querystring = require('querystring');
var http = require('http');
var host = 'hack2020.eastus.cloudapp.azure.com';

const LaunchRequestHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'LaunchRequest';
    },
    handle(handlerInput) {
        const speakOutput = 'Welcome to Natural Language Coding, you can start your application ';
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};

const HasApplicationLaunchRequestHandler = {
    canHandle(handlerInput) {

        const attributesManager = handlerInput.attributesManager;
        const sessionAttributes = attributesManager.getSessionAttributes() || {};

        const ApplicationName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;

        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'LaunchRequest'

            && ApplicationName;
    },
    handle(handlerInput) {

        const attributesManager = handlerInput.attributesManager;
        const sessionAttributes = attributesManager.getSessionAttributes() || {};

        const ApplicationName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;

        // TODO:: Use the settings API to get current date and then compute how many days until user's birthday
        // TODO:: Say Happy birthday on the user's birthday

        const speakOutput = `Welcome back. It looks like you have started with project ${ApplicationName}.`;

        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};


const GenerateApplicationIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'GenerateProjectIntent';
    },
    async handle(handlerInput) {
        const ApplicationName1 = handlerInput.requestEnvelope.request.intent.slots.ProjectName.value;
        //console.log(responseString);
        
        const ApplicationName = titleCase(ApplicationName1);
        
        //var ApplicationName = ApplicationName1.split(" ").join("_");
        
        const Action = 'GenerateProject' ;   
        const inputFormat = {
            ProjectName: ApplicationName
        };
    
        const input = JSON.stringify(inputFormat);
    
        const request = {
            "Action": Action,
            "Input": input
        }
    
        const response = await ExecuteRestapi(request);
        
        const attributesManager = handlerInput.attributesManager;
        
        //const applicationAttributes = {
        //"ApplicationName" : ApplicationName
        //};
        
        //attributesManager.setPersistentAttributes(applicationAttributes);
        
        //await attributesManager.savePersistentAttributes();
        const sessionAttributes = handlerInput.attributesManager.getSessionAttributes();
        sessionAttributes.ApplicationName = ApplicationName;
        sessionAttributes.ClassName = '';
        sessionAttributes.FunctionName = '';
        await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);
        
        const speakOutput = response;
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};

  
const GenerateClassIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'GenerateClassIntent';
    },
    async handle(handlerInput) {
        const ClassName1 = handlerInput.requestEnvelope.request.intent.slots.ClassName.value;
        //const Accessor=handlerInput.requestEnvelope.request.intent.slots.Accessor.value;
        const Accessor='public';
        var ProjectName = '';
        
        const ClassName = titleCase(ClassName1);
        
        const attributesManager = handlerInput.attributesManager;
        const sessionAttributes = attributesManager.getSessionAttributes() || {};

        const ApplicationName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
        
        if (sessionAttributes.ApplicationName) {
            ProjectName = sessionAttributes.ApplicationName;
        }
            
        const Action = 'GenerateClass' ;   
        const inputFormat = {
            ClassName: ClassName,
            Accessor: Accessor,
            ProjectName: ProjectName
        };
        
        const input = JSON.stringify(inputFormat);
        
        const request = {
            "Action": Action,
            "Input": input
        }
        
        const response = await ExecuteRestapi(request);
        
        
        sessionAttributes.ClassName = ClassName;
        sessionAttributes.FunctionName = '';
        await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

        const reprompt = 'Sorry I could not understand that. Could you please repeat!';
        const speakOutput = response ;
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};

const AddPropertyToClassIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'AddPropertyToClassIntent';
    },
    async handle(handlerInput) {
        
    const PropertyName1 = handlerInput.requestEnvelope.request.intent.slots.PropertyName.value;
    const PropertyName = titleCase(PropertyName1);
    //const ReturnType = handlerInput.requestEnvelope.request.intent.slots.ReturnType.value;
    const Accessor='public';
    //const ReturnType1 = 'int';
    
    function analyseReturnType(string) {
      var type;
      if(string.includes('Identity')){
        type = 'int';
      }
      else{
        type = 'string';
      }
      return type;
  
    }
    
    const ReturnType = analyseReturnType(PropertyName);
    
    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};

    const ProjectName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
    const ClassName = sessionAttributes.hasOwnProperty('ClassName') ? sessionAttributes.ClassName : 0;
        
    const Action = 'GenerateProperty' ;   
    const inputFormat = {
        PropertyName: PropertyName,
        ReturnType: ReturnType,
        Accessor: Accessor,
        ProjectName: ProjectName,
        ClassName: ClassName
    };

    const input = JSON.stringify(inputFormat);

    const request = {
        "Action": Action,
        "Input": input
    }

    const response = await ExecuteRestapi(request);
    
        //sessionAttributes.FunctionName = FunctionName;
        //await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

        console.log('Before Speak:');
        const speakOutput = response ;
        console.log('After Speak:');
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};

const FilterIntentIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'FilterIntent';
    },
    async handle(handlerInput) {
        
    const PropertyName1 = handlerInput.requestEnvelope.request.intent.slots.PropertyName.value;
    const PropertyName = titleCase(PropertyName1);

    const PropertyValue = handlerInput.requestEnvelope.request.intent.slots.PropertyValue.value;
    
    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};

    const ProjectName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
    const ClassName = sessionAttributes.hasOwnProperty('ClassName') ? sessionAttributes.ClassName : 0;
    
    var VariableName = ClassName + 'List';
        
    const Action = 'AddFilter' ;   
    const inputFormat = {
        VariableName: VariableName,
        ClassName: ClassName,
        ProjectName: ProjectName,
        PropertyName: PropertyName,
        PropertyValue: PropertyValue
    };

    const input = JSON.stringify(inputFormat);

    const request = {
        "Action": Action,
        "Input": input
    }

    const response = await ExecuteRestapi(request);
    
        //sessionAttributes.FunctionName = FunctionName;
        //await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

        console.log('Before Speak:');
        const speakOutput = response ;
        console.log('After Speak:');
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};

function titleCase(string) {
    var sentence = string.toLowerCase().split(" ");
    for(var i=0; i<sentence.length; i++){
      sentence[i] = sentence[i][0].toUpperCase() +sentence[i].slice(1);
    }
    
    var result = sentence.join("");
    return result;
}


function performRequest(endpoint, method, data) {
    return new Promise(((resolve, reject) => {
        var dataString = JSON.stringify(data);
        var headers = {};
        
        if (method === 'GET') {
          endpoint += '?' + querystring.stringify(data);
        }
        else {
          headers = {
            'Content-Type': 'application/json',
            'Content-Length': dataString.length
          };
        }
        var options = {
          host: host,
          port: 80,
          path: endpoint,
          method: method,
          headers: headers
        };
      
        var req = http.request(options, function(res) {
          res.setEncoding('utf-8');
      
          var responseString = '';
      
          res.on('data', function(data) {
            responseString += data;
          });
      
          res.on('end', function() {
            console.log(responseString);
            resolve(JSON.parse(responseString));
          });

          res.on('error',(error) => {
            reject(error);
          })
        });
      
        req.write(dataString);
        req.end();
      }));
}

  async function ExecuteRestapi(request){

    var response = await performRequest('/api/Application/generate', 'POST', request) ;
    var output;

    const status = response.status;
    if(status === 'success')
    {
        output = response.result;
        console.log('Logged in:', output);
    }
    else{
        output = response.result;
        console.log('Failed :', output);
    }
    
    return output;
  }

const GenerateFunctionIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'GenerateFunctionIntent';
    },
    async handle(handlerInput) {
        
    const FunctionName1 = handlerInput.requestEnvelope.request.intent.slots.FunctionName.value;
    //const ReturnType = handlerInput.requestEnvelope.request.intent.slots.ReturnType.value;
    const Accessor='public';
    const ReturnType='void';
    
    var FunctionName = FunctionName1.split(" ").join("_");
    
    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};

    const ProjectName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
    const ClassName = sessionAttributes.hasOwnProperty('ClassName') ? sessionAttributes.ClassName : 0;
        
    const Action = 'GenerateFunction' ;   
    const inputFormat = {
        FunctionName: FunctionName,
        ReturnType: ReturnType,
        Accessor: Accessor,
        ProjectName: ProjectName,
        ClassName: ClassName
    };

    const input = JSON.stringify(inputFormat);

    const request = {
        "Action": Action,
        "Input": input
    }
    
        const response = await ExecuteRestapi(request);
    
        sessionAttributes.FunctionName = FunctionName;
        await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

        console.log('Before Speak:');
        const speakOutput = response;
        console.log('After Speak:');
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};
    
const AllowUserEntriesIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'AllowUserEntriesIntent';
    },
    async handle(handlerInput) {
        
    
    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};

    const ProjectName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
    const ClassName = sessionAttributes.hasOwnProperty('ClassName') ? sessionAttributes.ClassName : 0;
    
    var VariableName = ClassName + 'List';
        
    const Action = 'FetchDetails' ;   
    const inputFormat = {
        VariableName: VariableName,
        ProjectName: ProjectName,
        ClassName: ClassName
    };

    const input = JSON.stringify(inputFormat);

    const request = {
        "Action": Action,
        "Input": input
    }

    const response = await ExecuteRestapi(request);

        console.log('Before Speak:');
        const speakOutput = response;
        console.log('After Speak:');
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};



const StorageIntentIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'StorageIntent';
    },
    async handle(handlerInput) {
        
    //const FunctionName1 = handlerInput.requestEnvelope.request.intent.slots.FunctionName.value;
    //const ReturnType = handlerInput.requestEnvelope.request.intent.slots.ReturnType.value;
    //const Accessor='public';
    //const ReturnType='void';
    
    //var FunctionName = FunctionName1.split(" ").join("_");

    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};

    const ProjectName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
    const ClassName = sessionAttributes.hasOwnProperty('ClassName') ? sessionAttributes.ClassName : 0;
    
    const VariableName = ClassName + "List";
        
    const Action = 'GenerateStorage' ;   
    const inputFormat = {
        ProjectName: ProjectName,
        ClassName: ClassName,
        VariableName:VariableName
    };

    const input = JSON.stringify(inputFormat);

    const request = {
        "Action": Action,
        "Input": input
    }

    const response = await ExecuteRestapi(request);
    
        //sessionAttributes.FunctionName = FunctionName;
        //await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

        console.log('Before Speak:');
        const speakOutput = response;
        console.log('After Speak:');
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};


const GetDataFromDBIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'GetDataFromDBIntent';
    },
    async handle(handlerInput) {

    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};

    const ProjectName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
    const ClassName = sessionAttributes.hasOwnProperty('ClassName') ? sessionAttributes.ClassName : 0;
    
    const VariableName = ClassName + "List";
        
    const Action = 'GetListFromDB' ;   
    const inputFormat = {
        VariableName: VariableName,
        ProjectName: ProjectName,
        ClassName: ClassName
    };


    const input = JSON.stringify(inputFormat);

    const request = {
        "Action": Action,
        "Input": input
    }

    const response = await ExecuteRestapi(request);
    
        //sessionAttributes.FunctionName = FunctionName;
        //await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

        console.log('Before Speak:');
        const speakOutput = response;
        console.log('After Speak:');
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};


const DisplayToConsoleIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'DisplayToConsoleIntent';
    },
    async handle(handlerInput) {

    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};

    const ProjectName = sessionAttributes.hasOwnProperty('ApplicationName') ? sessionAttributes.ApplicationName : 0;
    const ClassName = sessionAttributes.hasOwnProperty('ClassName') ? sessionAttributes.ClassName : 0;
    
    const VariableName = ClassName + "List";
        
    const Action = 'DisplayToConsole' ;   
    const inputFormat = {
        VariableName: VariableName,
        ClassName: ClassName,
        ProjectName: ProjectName
    };


    const input = JSON.stringify(inputFormat);

    const request = {
        "Action": Action,
        "Input": input
    }

    const response = await ExecuteRestapi(request);
    
        //sessionAttributes.FunctionName = FunctionName;
        //await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

        console.log('Before Speak:');
        const speakOutput = response;
        console.log('After Speak:');
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};



const cSharpGreetingIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'cSharpGreetingIntent';
    },
    async handle(handlerInput) {
    const Language = 'cSharp';
    
    const attributesManager = handlerInput.attributesManager;
    const sessionAttributes = attributesManager.getSessionAttributes() || {};
    
    sessionAttributes.Language = Language;
    await handlerInput.attributesManager.setSessionAttributes(sessionAttributes);

    const response = 'Ok. I am ready to code for you in  C Sharp';    
    //'Ok. I am ready to code for you in  C Sharp';

    console.log('Before Speak:');
    const speakOutput = response;
    console.log('After Speak:');
    return handlerInput.responseBuilder
        .speak(speakOutput)
        .reprompt(speakOutput)
        .getResponse();
    }
};


const GenerateSQLCommandIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'GenerateSQLCommandIntent';
    },
    async handle(handlerInput) {
        const QueryText = handlerInput.requestEnvelope.request.intent.slots.QueryText.value;
        const CommandType = 'sqlcommand';
            
        const Action = 'GenerateSQLCommand' ;   
        const inputFormat = {
            CommandType: CommandType,
            QueryText: QueryText
        };
        
        const input = JSON.stringify(inputFormat);
        
        const request = {
            "Action": Action,
            "Input": input
        }
        
        const response = await ExecuteRestapi(request);
        
        const speakOutput = response;
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};


const HelpIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && Alexa.getIntentName(handlerInput.requestEnvelope) === 'AMAZON.HelpIntent';
    },
    handle(handlerInput) {
        const speakOutput = 'You can say hello to me! How can I help?';

        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};
const CancelAndStopIntentHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest'
            && (Alexa.getIntentName(handlerInput.requestEnvelope) === 'AMAZON.CancelIntent'
                || Alexa.getIntentName(handlerInput.requestEnvelope) === 'AMAZON.StopIntent');
    },
    handle(handlerInput) {
        const speakOutput = 'Goodbye!';
        return handlerInput.responseBuilder
            .speak(speakOutput)
            .getResponse();
    }
};
const SessionEndedRequestHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'SessionEndedRequest';
    },
    handle(handlerInput) {
        // Any cleanup logic goes here.
        return handlerInput.responseBuilder.getResponse();
    }
};

// The intent reflector is used for interaction model testing and debugging.
// It will simply repeat the intent the user said. You can create custom handlers
// for your intents by defining them above, then also adding them to the request
// handler chain below.
const IntentReflectorHandler = {
    canHandle(handlerInput) {
        return Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest';
    },
    handle(handlerInput) {
        const intentName = Alexa.getIntentName(handlerInput.requestEnvelope);
        const speakOutput = `You just triggered ${intentName}`;

        return handlerInput.responseBuilder
            .speak(speakOutput)
            //.reprompt('add a reprompt if you want to keep the session open for the user to respond')
            .getResponse();
    }
};

// Generic error handling to capture any syntax or routing errors. If you receive an error
// stating the request handler chain is not found, you have not implemented a handler for
// the intent being invoked or included it in the skill builder below.
const ErrorHandler = {
    canHandle() {
        return true;
    },
    handle(handlerInput, error) {
        console.log(`~~~~ Error handled: ${error.stack}`);
        const speakOutput = `Sorry, I had trouble doing what you asked. Please try again.`;

        return handlerInput.responseBuilder
            .speak(speakOutput)
            .reprompt(speakOutput)
            .getResponse();
    }
};

const LoadApplicationInterceptor = {
    async process(handlerInput) {
        const attributesManager = handlerInput.attributesManager;
        const sessionAttributes = await attributesManager.getPersistentAttributes() || {};
        
        attributesManager.setSessionAttributes(sessionAttributes);
    }
};

const PersistenceSavingResponseInterceptor = {
    
    process(handlerInput) {
        const attributesManager = handlerInput.attributesManager;
        const sessionAttributes = attributesManager.getSessionAttributes() || {};
        
        return new Promise((resolve, reject) => {
            handlerInput.attributesManager.savePersistentAttributes(sessionAttributes)
                .then(() => {
                    resolve();
                })
                .catch((error) => {
                    reject(error);
                });
        });
    }
};



// The SkillBuilder acts as the entry point for your skill, routing all request and response
// payloads to the handlers above. Make sure any new handlers or interceptors you've
// defined are included below. The order matters - they're processed top to bottom.
exports.handler = Alexa.SkillBuilders.custom()
    .withPersistenceAdapter(
        new persistenceAdapter.S3PersistenceAdapter({bucketName:process.env.S3_PERSISTENCE_BUCKET})
    )
    .addRequestHandlers(
        HasApplicationLaunchRequestHandler,
        LaunchRequestHandler,
        GenerateApplicationIntentHandler,
        GenerateClassIntentHandler,
        GenerateFunctionIntentHandler,
        GenerateSQLCommandIntentHandler,
        AddPropertyToClassIntentHandler,
        StorageIntentIntentHandler,
        cSharpGreetingIntentHandler,
        AllowUserEntriesIntentHandler,
        FilterIntentIntentHandler,
        GetDataFromDBIntentHandler,
        DisplayToConsoleIntentHandler,
        HelpIntentHandler,
        CancelAndStopIntentHandler,
        SessionEndedRequestHandler,
        IntentReflectorHandler // make sure IntentReflectorHandler is last so it doesn't override your custom intent handlers
    ) 
    .addRequestInterceptors(
        LoadApplicationInterceptor)
    .addResponseInterceptors(
        PersistenceSavingResponseInterceptor)
    .addErrorHandlers(
        ErrorHandler,
    )
    .lambda();
