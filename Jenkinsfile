pipeline {
    agent any

    environment {
        SONAR_TOKEN = credentials('sonarcloud-token')
    }

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

        stage('Code Quality') {
            steps {
                echo 'Running SonarCloud analysis...'
                bat '''
                dotnet sonarscanner begin /k:"c5982d24dd603f7f7be6aeeb728368697d8f6fd8" /o:"Sanyam4880" /d:sonar.login=%SONAR_TOKEN%
                dotnet build robot4-controller-api.csproj
                dotnet sonarscanner end /d:sonar.login=%SONAR_TOKEN%
                '''
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