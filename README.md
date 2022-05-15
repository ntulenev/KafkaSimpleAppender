# KafkaSimpleAppender

UI for sending simple messages to Kafka

Current version supports messages with several key types (string, json, long and no key) and string or json payload.
Data can be added message by message via UI or can be loaded from file.

![Details](Example.png)

Config example:

```json
  "BootstrapConfiguration": {
    "BootstrapServers": [
      "kafka:9092"
    ],
    "Username": "user123",
    "Password": "pwd123",
    "SecurityProtocol": "SaslPlaintext",
    "SASLMechanism": "ScramSha512",
    "MessageMaxBytes": 10000000
  },
  "FileLoaderConfiguration": {
    "FileKeyField": "Key",
    "FileValueField": "Value"
  }
```

| Parameter name | Description   |
| -------------- | ------------- |
| BootstrapServers | List of kafka cluster servers, like "kafka-test:9092"  |
| Username | SASL username (optional)  |
| Password | SASL password (optional)  |
| SecurityProtocol | Protocol used to communicate with brokers (Plaintext,Ssl,SaslPlaintext,SaslSsl) (optional)  |
| SASLMechanism | SASL mechanism to use for authentication (Gssapi,Plain,ScramSha256,ScramSha512,OAuthBearer) (optional)  |
| MessageMaxBytes | Maximum Kafka protocol request message size (optional)  |
| FileKeyField | Key field name in file  |
| FileValueField | Value field name in file  |

File examples

File with keys and values:

```json
[
    {
        "Key": "Apple",
        "Value": "Large"
    },
    {
        "Key": "Orange",
        "Value": "Medium"
    }
]
```

File only with keys:

```json
[
    {
        "Value": "Large"
    },
    {
        "Value": "Medium"
    }
]
```
