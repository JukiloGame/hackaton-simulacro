import axios from "axios";

export const childApiInstance = axios.create({
  baseURL: "http://localhost:5098/",
  timeout: 1000,
});