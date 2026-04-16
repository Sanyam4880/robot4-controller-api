pipeline {
    agent any

    stages {

        stage('Build') {
            steps {
                echo 'Restoring dependencies...'
                bat 'dotnet restore'

                echo 'Building project in Release mode...'
                bat 'dotnet build -c Release --no-restore'

                echo 'Creating deployable build artifact for main API project only...'
                bat 'if exist build_artifact rmdir /S /Q build_artifact'
                bat 'dotnet publish robot4-controller-api.csproj -c Release -o build_artifact --no-build'

                echo 'Showing generated build artifact files...'
                bat 'dir build_artifact'
            }
        }

        stage('Test') {
            steps {
                echo 'Running automated tests using xUnit...'
                bat 'dotnet test'
            }
        }

    }

    post {
        success {
            echo 'Build and Test stages completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}