import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

export const ordersHub = new HubConnectionBuilder()
  .withUrl("/ordershub")
  .configureLogging(LogLevel.Information)
  .build();
