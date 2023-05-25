package main

import (
	"bytes"
	"encoding/json"
	"io"
	"log"
	"net/http"
)

type Config struct {
	ApiUrl   string
	UserId   uint
	Username string
	Password string
	Jwt      string
}

const (
	get    string = "GET"
	post   string = "POST"
	put    string = "PUT"
	delete string = "DELETE"
)

func writeBody[T any](body T) []byte {
	rawBody, err := json.Marshal(body)
	if err != nil {
		log.Fatal("Error marshalling: ", err)
	}
	return rawBody
}

func request(method string, url string, body []byte) *http.Response {
	client := &http.Client{}

	var reqBody io.Reader = nil

	if method == "POST" || method == "PUT" {
		reqBody = bytes.NewReader(body)
	}

	req, err := http.NewRequest(method, config.ApiUrl+url, reqBody)
	if err != nil {
		log.Fatal("Error creating request: ", err)
	}

	req.Header.Set("Content-Type", "application/json")

	if config.Jwt != "" {
		req.AddCookie(&http.Cookie{Name: "X-Access-Token", Value: config.Jwt})
	}

	res, err := client.Do(req)
	if err != nil {
		log.Fatal("Error doing request", err)
	}

	return res
}

func readBody[T any](res *http.Response) *T {
	resBody, err := io.ReadAll(res.Body)
	if err != nil {
		log.Fatal("Error reading response body: ", err)
	}
	res.Body.Close()

	body := new(T)

	err = json.Unmarshal(resBody, body)
	if err != nil {
		log.Fatal("Error unmarshalling", err)
	}

	return body
}
