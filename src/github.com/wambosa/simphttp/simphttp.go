package simphttp

import (
	"net/http"
	"io/ioutil"
	"encoding/json"
)

func GetString(url string) (string, error) {

	resp, err := http.Get(url)

	if err != nil {return "", err}

	defer resp.Body.Close()

	buf, err := ioutil.ReadAll(resp.Body)

	if err != nil {return "", err}

	return string(buf), err
}

func GetJson(url string) (map[string]interface{}, error) {

	raw, err := GetString(url)

	if err != nil { return nil, err}

	var newMap map[string]interface{}

	json.Unmarshal([]byte(raw), &newMap)

	return newMap, err
}