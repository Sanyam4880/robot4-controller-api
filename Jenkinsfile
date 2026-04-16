stage('Build') {
    steps {
        echo 'Restoring dependencies...'
        bat 'dotnet restore'

        echo 'Building project in Release mode...'
        bat 'dotnet build -c Release --no-restore'

        echo 'Creating deployable build artifact...'
        bat 'if exist build_artifact rmdir /S /Q build_artifact'
        bat 'dotnet publish -c Release -o build_artifact --no-build'

        echo 'Showing generated build artifact files...'
        bat 'dir build_artifact'
    }
}