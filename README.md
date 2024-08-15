# RabbitMQ Publisher and Consumer in C#

Note: This repository was written only by me and there are used many principles within differ approaches.

This repository contains a basic implementation of a RabbitMQ Publisher and Consumer using C#. It demonstrates how to use RabbitMQ to send and receive messages between different parts of a system, utilizing a common connection manager to streamline the creation of connections and channels.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)

## Overview

RabbitMQ is a message broker that allows applications to communicate by sending and receiving messages. This repository provides a basic example of how to implement a RabbitMQ publisher and consumer in C#, utilizing a shared connection manager to handle RabbitMQ connections efficiently.

## Features

- **Publisher**: Sends messages provided by API to a specified queue.
- **Consumer**: Listens to a specified queue and processes incoming messages.
- **Common Connection Manager**: Simplifies connection management by encapsulating the logic for creating and managing RabbitMQ connections and channels.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0)
- [RabbitMQ Server](https://www.rabbitmq.com/download.html) (version 3.x)

Ensure that RabbitMQ is installed and running on your local machine or accessible from your environment.
