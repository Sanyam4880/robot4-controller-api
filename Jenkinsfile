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

        stage('Security') {
            steps {
                echo 'Running Trivy security scan...'
                bat '''
                C:\\trivy\\trivy.exe fs --severity HIGH,CRITICAL --exit-code 0 --skip-dirs bin,obj .
                '''
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running SonarCloud analysis...'
                bat """
                C:\\sonar-scanner\\dotnet-sonarscanner begin /o:"sanyam4880" /k:"Sanyam4880_robot4-controller-api" /d:sonar.token=%SONAR_TOKEN%
                dotnet build SIT3314.2C.sln
                C:\\sonar-scanner\\dotnet-sonarscanner end /d:sonar.token=%SONAR_TOKEN%
                """
            }
        }
    }

    post {
        success {
            echo 'Build, Test, Security and Code Quality completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}