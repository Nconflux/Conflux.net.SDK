rem packing web3 and dependencies
del /S *.*.nupkg
del /S *.*.snupkg
SET releaseSuffix=
SET targetNet35=false

cd Nethereum.Hex
CALL :restorepack
cd ..

cd Nethereum.ABI
CALL :restorepack
cd ..

cd Nethereum.JsonRpc.Client
CALL :restorepack
cd ..

cd Nethereum.RPC
CALL :restorepack
cd ..

cd Nethereum.Web3
CALL :restorepack
cd ..

cd Nethereum.StandardToken*
CALL :restorepack
cd ..

cd Nethereum.JsonRpc.IpcClient*
CALL :restorepack
cd ..

cd Nethereum.JsonRpc.RpcClient*
CALL :restorepack
cd ..

cd Nethereum.JsonRpc.WebSocketClient*
CALL :restorepack
cd ..

cd Nethereum.KeyStore*
CALL :restorepack
cd ..

cd Nethereum.ENS*
CALL :restorepack
cd ..

cd Nethereum.Quorum*
CALL :restorepack
cd ..

cd Nethereum.Geth*
CALL :restorepack
cd ..

cd Nethereum.Contracts*
CALL :restorepack
cd ..

cd Nethereum.RLP*
CALL :restorepack
cd ..

cd Nethereum.Signer
CALL :restorepack
cd ..

cd Nethereum.Signer.AzureKeyVault
CALL :restorepack
cd ..

cd Nethereum.Signer.Ledger
CALL :restorepack
cd ..

cd Nethereum.Signer.Trezor
CALL :restorepack
cd ..

cd Nethereum.Util*
CALL :restorepack
cd ..

cd Nethereum.Uport*
CALL :restorepack
cd ..

cd Nethereum.HDWallet
CALL :restorepack
cd ..

cd Nethereum.Parity*
CALL :restorepack
cd ..

cd Nethereum.Accounts*
CALL :restorepack
cd ..

cd Nethereum.Parity.Reactive
CALL :restorepack
cd..

cd Nethereum.RPC.Reactive
CALL :restorepack
cd..

cd Nethereum.Model
CALL :restorepack
cd..

cd Nethereum.StandardNonFungibleTokenERC721
CALL :restorepack
cd..

cd Nethereum.Pantheon
CALL :restorepack
cd..

cd Nethereum.RSK
CALL :restorepack
cd..

cd Nethereum.BlockchainProcessing
CALL :restorepack
cd..


setlocal
set DIR=%~dp0
set OUTPUTDIR=%~dp0\packages
for /R %DIR% %%a in (*.nupkg) do xcopy "%%a" "%OUTPUTDIR%"
xcopy *.nupkg packages /s /y

for /R %DIR% %%a in (*.snupkg) do xcopy "%%a" "%OUTPUTDIR%"
xcopy *.snupkg packages /s /y

EXIT /B %ERRORLEVEL%

:restorepack
dotnet restore /property:ReleaseSuffix=%releaseSuffix% /property:TargetNet35=%targetNet35%
dotnet build -c Release /property:TargetNet35=%targetNet35% /property:ReleaseSuffix=%releaseSuffix%
dotnet pack -c Release --include-symbols -p:SymbolPackageFormat=snupkg /property:TargetNet35=%targetNet35% /property:ReleaseSuffix=%releaseSuffix%
EXIT /B 0