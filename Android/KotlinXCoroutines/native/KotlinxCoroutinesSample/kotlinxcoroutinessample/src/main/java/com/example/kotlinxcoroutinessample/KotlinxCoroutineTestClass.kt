package com.example.kotlinxcoroutinessample

import android.widget.TextView
import kotlinx.coroutines.*

class KotlinxCoroutineTestClass(private val textView: TextView) {

    fun testGlobalScope() {
        GlobalScope.launch { // launch a new coroutine in background and continue
            delay(1000L)
            addTextToTextView("Kotlinx Coroutine!\n")
        }
        addTextToTextView("Hello, ")
    }

    fun testScopeBuilder(){
        runBlocking { // this: CoroutineScope
            launch {
                delay(200L)
                addTextToTextView("\n2. Task from runBlocking")
            }

            coroutineScope { // Creates a coroutine scope
                launch {
                    delay(500L)
                    addTextToTextView("\n3. Task from nested launch")
                }

                delay(100L)
                addTextToTextView("\n1. Task from coroutine scope") // This line will be printed before the nested launch
            }

            addTextToTextView("\n4. Coroutine scope is over") // This line is not printed until the nested launch completes
        }
    }

    private fun addTextToTextView(text: String) {
        textView.append(text)
    }
}