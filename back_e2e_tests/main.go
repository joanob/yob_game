package main

import (
	"fmt"
	"log"
	"os"

	"github.com/joho/godotenv"
)

var config Config

func main() {
	fmt.Println("Testing e2e")
	config = Config{}

	// load base api url from .env
	godotenv.Load(".env")
	apiUrl := os.Getenv("API_URL")
	if apiUrl == "" {
		log.Fatal("API_URL environment variable not set")
	}

	config.ApiUrl = apiUrl

	// Tests
	testAccountCreation()

	fmt.Println("All tests passed successfully")
}
