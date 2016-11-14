package expect

import (
    "fmt"
    "testing"
)

type TestCase struct {
    T *testing.T
    Value interface{}
    Map map[string]interface{}
}

func (tc *TestCase) Property(propertyName string) (*TestCase){
    
    // todo: ensure that Value type is map[string]interface{} first
    
    if tc.Map == nil {
        tc.Map = tc.Value.(map[string]interface{})
    }
    
    tc.Value = tc.Map[propertyName]
    
    return tc
}

func (tc *TestCase) Truthy() (bool) {
    
    r := false
    
    //todo: switch on type to extract value. complex types will need to be broken down into maps
    //note: this is not trustworthy yet. will write test for the assertion library :p
    if len(toString(tc.Value)) > 0 {r = true}
    
    if !r {tc.T.Errorf("Expect Truthy: true | actual: %v\n", tc.Value)}
    
    return r
}

func (tc *TestCase) Falsy() (bool) {
    
    r := false
    
    if len(toString(tc.Value)) == 0 {r = true}
    
    if !r {tc.T.Errorf("Expect Falsy: false | actual: %v\n", tc.Value)}
    
    return r
}

func (tc *TestCase) ToBe(value interface{}) (bool) {
    
    r := false
    
    if toString(value) == toString(tc.Value) {r = true}
    
    if !r {tc.T.Errorf("Expect ToBe: %v | actual: %v\n", value, tc.Value)}
    
    return r
}

func toString(value interface{}) (string){
    return fmt.Sprintf("%v", value)
}