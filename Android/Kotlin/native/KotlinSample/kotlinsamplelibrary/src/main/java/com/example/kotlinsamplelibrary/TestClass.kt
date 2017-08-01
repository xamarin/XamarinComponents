package com.example.kotlinsamplelibrary

import java.util.ArrayList

class TestClass {
    fun Test(array: Array<String>): List<String> {
        val list = ArrayList<String>()
        array.forEach { str -> list.add(str) }
        return list
    }
}
