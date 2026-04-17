pipeline {
    agent any

    environment {
        SONAR_TOKEN = credentials('sonarcloud-token')
    }

    stages {
        stage('Build') {
            steps {
                echo 'Restoring dependencies...'
                bat 'dotnet restore SIT3314.2C.sln'

                echo 'Building main project in Release mode...'
                bat 'dotnet build robot4-controller-api\\robot4-controller-api.csproj -c Release --no-restore'

                echo 'Creating deployable build artifact...'
                bat 'if exist build_artifact rmdir /S /Q build_artifact'
                bat 'dotnet publish robot4-controller-api\\robot4-controller-api.csproj -c Release -o build_artifact --no-build'

                echo 'Artifact files:'
                bat 'dir build_artifact'
            }
        }

        stage('Test') {
            steps {
                echo 'Running automated xUnit tests...'
                bat 'dotnet test robot4-controller-api.Tests\\robot4-controller-api.Tests.csproj'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running SonarCloud analysis...'
                bat """
                dotnet sonarscanner begin /k:"c5982d24dd603f7f7be6aeeb728368697d8f6fd8" /o:"Sanyam4880" /d:sonar.login=%SONAR_TOKEN%
                dotnet build SIT3314.2C.sln
                dotnet sonarscanner end /d:sonar.login=%SONAR_TOKEN%
                """
            }
        }
    }

    post {
        success {
            echo 'Build, Test and Code Quality completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}