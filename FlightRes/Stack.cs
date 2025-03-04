using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace FlightRes;

public class Stack<T>
{
    public T[] stack;
    public int size,capacity;

    public Stack(int cap){
        capacity = cap;
        stack = new T[capacity];
        size = 0;
        
    }
    public void push(T data){
        if(capacity==size){
            capacity = capacity*2;
            T[] newStack = new T[capacity];
            Array.Copy(stack,newStack,size);
            stack = newStack;
        }
        else{
            stack[size]=data;
            size++;
        }
    }
    public void pop(){
        size--; 
    }
}
