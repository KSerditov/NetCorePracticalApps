"use strict";
const testConst = " 4.4.2022";
const COLOR_RED = "#F00";
let message;
message = "Привет, мир!" + testConst;
alert(message);

alert("не число" / 2 + 5); // NaN

// символ "n" в конце означает, что это BigInt
const bigInt = 1234567890123456789012345678901234567890n;

let str = "Привет";
let str2 = 'Одинарные кавычки тоже подойдут';
let phrase = `Обратные кавычки позволяют встраивать переменные ${str}`;

let isGreater = 4 > 1;

alert(isGreater); // true (результатом сравнения будет "да")

let age;

alert(age); // выведет "undefined"

let age2 = null;


typeof undefined // "undefined"

typeof 0 // "number"

typeof 10n // "bigint"

typeof true // "boolean"

typeof "foo" // "string"

typeof Symbol("id") // "symbol"

typeof Math // "object"  (1)

typeof null // "object"  (2)

typeof alert // "function"  (3)