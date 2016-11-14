package confman_test

import (
    "os"
    "testing"
    "path/filepath"
    "github.com/wambosa/expect"
    "github.com/wambosa/confman"
)

func Test_GetThisFolder_returns_executable_root_directory(t *testing.T){
    
    correctDir, _ := filepath.Abs(filepath.Dir(os.Args[0]))
    
    expecting := expect.TestCase {
        T: t,
        Value: confman.GetThisFolder(),
    }
    
    expecting.ToBe(correctDir)
}

// todo: add several more tests that connfirm that the type fields are castable back and nested types are properly converted as well. pointers. etc. for now this meets the exact purpose it is used for
func Test_StructToMap_when_passed_a_flat_definedType_then_converts_to_generic_map(t *testing.T){
    
    type zWarrior struct {
        Name string
        Children []string
        PowerLevel int
    }
    
    c := zWarrior {
        Name: "goku",
        Children: []string{"gohan", "goten", "pan"},
        PowerLevel: 9000,
    }
    
    m := confman.StructToMap(c)
    
    expecting := expect.TestCase {
        T: t,
        Value: m,
    }
    
    expecting.Property("Name").ToBe(c.Name)
    expecting.Property("Children").ToBe([]string{"gohan", "goten", "pan"})
    expecting.Property("PowerLevel").ToBe(9000)
}

func Test_LoadRaw_returns_string_successfully(t *testing.T){
    
    data, err := confman.LoadRaw("confman_test.json")
    
    if err != nil { t.Error(err) }
    
    expecting := expect.TestCase {
        T: t,
        Value: data,
    }
    
    expecting.Truthy()
}

func Test_LoadJson_when_passed_valid_json_then_returns_map(t *testing.T){
    
    m, err := confman.LoadJson("confman_test.json")
    
    if err != nil {t.Error(err)}
    
    expecting := expect.TestCase {
        T: t,
        Value: m,
    }
  
    expecting.Property("version").ToBe(-1)
    expecting.Property("name").ToBe("Galick Gun")
    expecting.Property("lastRunTime").ToBe("Saiyan Saga")
}

// todo: cleanup test save file
func Test_SaveJson_when_passed_valid_json_then_saves_proper_file(t *testing.T){
    
    json := map[string]interface{}{
        "version": 5000,
        "name": "Kamehameha",
        "lastRunTime": "Buu Saga",
    }
    
    err := confman.SaveJson("confman_test.testsave", json)
    
    if err != nil {t.Error(err)}
    
    m, err := confman.LoadJson("confman_test.testsave")
    
    if err != nil {t.Error(err)}
    
    expecting := expect.TestCase {
        T: t,
        Value: m,
    }
    
    expecting.Property("version").ToBe(5000)
    expecting.Property("name").ToBe("Kamehameha")
    expecting.Property("lastRunTime").ToBe("Buu Saga")
}