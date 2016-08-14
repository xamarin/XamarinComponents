package com.google.atap.tangocloudservice;

import android.os.IInterface;
import android.os.RemoteException;

interface ITangoCloudServiceCallbacks extends IInterface {
    void onTileAvailable(String paramString) throws RemoteException;

    void onCloudEvent(int paramInt1, int paramInt2) throws RemoteException;

    void onDebugEvent(String paramString1, String paramString2) throws RemoteException;

    void onTileUnload(String paramString) throws RemoteException;

    void onNavigationGraphAvailable(String paramString) throws RemoteException;

    abstract class Stub implements ITangoCloudServiceCallbacks {
    }
}
