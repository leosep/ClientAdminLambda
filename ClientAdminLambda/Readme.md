# AWS Lambda CRUD Application with .NET 7, DynamoDB, and Redis

This project demonstrates a simple AWS Lambda-based CRUD application built using .NET 7, Amazon DynamoDB for storage, and Amazon Redis for caching. The application allows basic CRUD operations for a "Client" entity and is designed to be serverless, using AWS Lambda functions triggered by HTTP requests via API Gateway.

## Project Overview

- **Serverless**: AWS Lambda functions to handle client requests.
- **Database**: Amazon DynamoDB for storing client data.
- **Caching**: Amazon Redis for caching client data to improve performance.
- **API Gateway**: Exposes the Lambda function as a REST API.

## Prerequisites

Before you start, make sure you have the following installed:

- **AWS CLI**: [Install AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html)
- **AWS Toolkit for Visual Studio**: [Install AWS Toolkit](https://aws.amazon.com/visualstudio/)
- **.NET 7 SDK**: [Install .NET 7](https://dotnet.microsoft.com/download)
- **Visual Studio 2022**: [Download Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)

## AWS Setup

Ensure you have the following AWS services configured:

- **AWS Lambda**: Create and configure a Lambda function.
- **Amazon DynamoDB**: Set up a DynamoDB table for storing client data.
- **Amazon Redis**: Set up an Elasticache Redis instance.
- **API Gateway**: Set up API Gateway to expose the Lambda functions as REST APIs.

You can find details on setting up these services in the AWS documentation:
- [AWS Lambda](https://docs.aws.amazon.com/lambda/latest/dg/welcome.html)
- [Amazon DynamoDB](https://docs.aws.amazon.com/dynamodb/latest/developerguide/Welcome.html)
- [Amazon ElastiCache (Redis)](https://docs.aws.amazon.com/AmazonElastiCache/latest/red-ug/WhatIs.html)
- [API Gateway](https://docs.aws.amazon.com/apigateway/latest/developerguide/welcome.html)
