package com.example.kotlinsamplelibrary

import java.util.ArrayList

class TestClass {

    companion object {
        private val isEven = IntPredicate { it % 2 == 0 }
    }

    var testProperty: String = "Not an empty string!"

    fun test(array: Array<String>): List<String> {
        val list = ArrayList<String>()
        array.forEach { str -> list.add(str) }
        return list
    }

    fun testUnsignedArray(): ULongArray {
        return ulongArrayOf(1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u, 18446744073709551614u, 18446744073709551615u)
    }

    fun testUnsigned(): UInt {
        var i: UInt = 4294967294u
        i = i + 1u
        return i
    }

    fun testIsEven(i: Int): String {
        return "Is $i even? - ${isEven.accept(i)}"
    }

    private fun interface IntPredicate {
        fun accept(i: Int): Boolean
    }

    fun testBitOperations() : String {
        val number = "1010000".toUInt(radix = 2)
        return "countOneBits:${number.countOneBits()},\n" +
               "countTrailingZeroBits:${number.countTrailingZeroBits()},\n" +
               "takeHighestOneBit:${number.takeHighestOneBit().toString(2)}\n\n"
    }
}
