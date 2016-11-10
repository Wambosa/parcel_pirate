package slack

import (
	"fmt"
	"github.com/wambosa/simphttp"
)

const baseUrl = "https://slack.com/api/"

var cachedToken string

type SlackConfig struct {
	Token string
	Channels []string
	LastRunTime string
}

func Configure(token string){
	cachedToken = token;
}

func ConvertSlackConfigToMap(conf SlackConfig) (map[string]interface{}){
	return map[string]interface{}{
		"token": conf.Token,
		"channels": conf.Channels, //todo: test this
		"lastRunTime":conf.LastRunTime,
	}
}

func GetChannels() ([]map[string]interface{}, error){

	method := fmt.Sprintf("%schannels.list?token=%s", baseUrl, cachedToken)

	response, err := simphttp.GetJson(method)

	if err != nil || response == nil {return nil, err}

	channels := make([]map[string]interface{}, len(response["channels"].([]interface{})))

	for i, channel := range response["channels"].([]interface{}){
		channels[i] = channel.(map[string]interface{})}

	return channels, nil
}

func GetChannelIds()([]string, error){

	chans, err := GetChannels()

	if err != nil {return nil, err}

	channelIds := make([]string, len(chans))

	for i, cha := range chans {
		channelIds[i] = cha["id"].(string)
	}

	return channelIds, nil
}

func GetChannelMessagesSinceLastRun(channel string, lastRunTime string)([]map[string]interface{}, error){

	method := fmt.Sprintf("%schannels.history?token=%s&channel=%s&oldest=%s", baseUrl, cachedToken, channel, lastRunTime)

	response, err := simphttp.GetJson(method)

	if(err != nil || response == nil){return nil, err}

	messages := make([]map[string]interface{}, len(response["messages"].([]interface{})))

	for i, message := range response["messages"].([]interface{}) {
		messages[i] = message.(map[string]interface{})}

	return messages, nil
}

func PostMessageToDefaultChannel(message string)(map[string]interface{}, error){
	// todo: do some testing to determine string escaping needs.
	// fmt.Sprintf("chat.postMessage?token=%s&username=ChOPS&channel=%s&text="
	return simphttp.GetJson(baseUrl + message)
}

func GetUserInfo(userId string)(map[string]interface{}, error){

	method := fmt.Sprintf("%susers.info?token=%s&user=%s", baseUrl, cachedToken, userId)

	response, err := simphttp.GetJson(method)

	if err != nil || response == nil {return nil, err}

	userInfo := response["user"].(map[string]interface{})

	return userInfo, nil
}