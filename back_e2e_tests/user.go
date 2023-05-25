package main

import (
	"fmt"
	"log"

	"github.com/google/uuid"
)

// First login should have empty company name

// Signup, login, session and set company name
func testAccountCreation() {
	fmt.Println("Testing user")

	// Test signup
	testSignupWithEmptyDataReturns400()
	testSignupWithUserReturns200()

	// Test session before login fails
	testSessionBeforeLoginReturns401()

	// Test login
	testFirstLoginWithUserReturns200UserWithEmptyCompanyName()
	testLoginWithWrongDataReturns400()

	// Test session after login succeeds
	testSessionReturns200User()

	// Test update company name
	testUpdateCompanyNameReturns200()
	testUpdateCompanyNameEmptyReturns400()
}

type authCmd struct {
	Username string
	Password string
}

// Signup

func testSignupWithEmptyDataReturns400() {
	cmd := authCmd{
		Username: "",
		Password: "",
	}

	res := request(post, "/user/signup", writeBody(cmd))
	if res.StatusCode != 400 {
		log.Fatal("Signup with empty data failed returning 400")
	}
}

func testSignupWithUserReturns200() {
	config.Username = uuid.NewString()
	config.Password = uuid.NewString()

	cmd := authCmd{
		Username: config.Username,
		Password: config.Password,
	}

	res := request(post, "/user/signup", writeBody(cmd))
	if res.StatusCode != 200 {
		log.Fatal("Signup with user failed returning 200")
	}
}

// Login

func testFirstLoginWithUserReturns200UserWithEmptyCompanyName() {
	cmd := authCmd{
		Username: config.Username,
		Password: config.Password,
	}

	res := request(post, "/user/login", writeBody(cmd))
	if res.StatusCode != 200 {
		log.Fatal("Login with user failed returning 200")
	}

	if len(res.Cookies()) != 2 {
		log.Fatal("Login cookies length not 2")
	}

	jwt := ""
	sessionStarted := ""
	for _, cookie := range res.Cookies() {
		if cookie.Name == "X-Access-Token" {
			if !cookie.HttpOnly {
				log.Fatal("Login cookie X-Access-Token not httponly")
			}
			jwt = cookie.Value
		}
		if cookie.Name == "X-Session-Started" {
			if cookie.HttpOnly {
				log.Fatal("Login cookie X-Session-Started is httponly")
			}
			sessionStarted = cookie.Value
		}
	}

	if jwt == "" {
		log.Fatal("Login cookie jwt not set")
	}
	if sessionStarted != "true" {
		log.Fatal("Login cookie session started not true")
	}
	config.Jwt = jwt

	body := readBody[User](res)
	if body.CompanyName != "" {
		log.Fatal("First login company name is not empty")
	}
	config.UserId = body.Id
}

func testLoginWithWrongDataReturns400() {
	cmd := authCmd{
		Username: uuid.NewString(),
		Password: uuid.NewString(),
	}

	res := request(post, "/user/login", writeBody(cmd))
	if res.StatusCode != 400 {
		log.Fatal("Login with wrong data failed returning 400")
	}
}

// Session

func testSessionBeforeLoginReturns401() {
	res := request(get, "/user/session", nil)
	if res.StatusCode != 401 {
		log.Fatal("Check session before login failed returning 401")
	}
}

func testSessionReturns200User() {
	res := request(get, "/user/session", nil)
	if res.StatusCode != 200 {
		log.Fatal("Check session failed returning 200")
	}

	body := readBody[User](res)
	if body.Id != config.UserId || body.Username != config.Username {
		log.Fatal("Check session body failed returning user")
	}
}

// Company name

type updateCompanyNameCmd struct {
	CompanyName string
}

// TODO:

func testUpdateCompanyNameReturns200() {
	cmd := updateCompanyNameCmd{
		CompanyName: uuid.NewString(),
	}

	res := request(post, "/user/company/name", writeBody(cmd))
	if res.StatusCode != 200 {
		log.Fatal("Update company name failed returning 200")
	}
}

func testUpdateCompanyNameEmptyReturns400() {
	cmd := updateCompanyNameCmd{
		CompanyName: "",
	}

	res := request(post, "/user/company/name", writeBody(cmd))
	if res.StatusCode != 400 {
		log.Fatal("Update company name empty failed returning 400")
	}
}
