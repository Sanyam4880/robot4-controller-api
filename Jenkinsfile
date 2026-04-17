pipeline {
    agent any

    environment {
        SONAR_TOKEN = credentials('sonarcloud-token')
    }

    stages {
<<<<<<< HEAD

        stage('Build') {
            steps {
                echo 'Restoring dependencies...'
                bat 'dotnet restore'

                echo 'Building solution...'
                bat 'dotnet build robot4-controller-api.sln -c Release'

                echo 'Publishing API artifact...'
                bat 'if exist build_artifact rmdir /S /Q build_artifact'
                bat 'dotnet publish robot4-controller-api.csproj -c Release -o build_artifact'
=======
        stage('Build') {
            steps {
                echo 'Restoring dependencies...'
                bat 'dotnet restore SIT3314.2C.sln'

                echo 'Building main project in Release mode...'
                bat 'dotnet build robot4-controller-api\\robot4-controller-api.csproj -c Release --no-restore'

                echo 'Creating deployable build artifact...'
                bat 'if exist build_artifact rmdir /S /Q build_artifact'
                bat 'dotnet publish robot4-controller-api\\robot4-controller-api.csproj -c Release -o build_artifact --no-build'
>>>>>>> dd8eaee (Final HD setup: fixed structure + tests + sonarcloud)

                echo 'Artifact files:'
                bat 'dir build_artifact'
            }
        }

        stage('Test') {
            steps {
<<<<<<< HEAD
                echo 'Running tests on solution...'
                bat 'dotnet test robot4-controller-api.sln --no-build'
=======
                echo 'Running automated xUnit tests...'
                bat 'dotnet test robot4-controller-api.Tests\\robot4-controller-api.Tests.csproj'
>>>>>>> dd8eaee (Final HD setup: fixed structure + tests + sonarcloud)
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running SonarCloud analysis...'
                bat """
                dotnet sonarscanner begin /k:"dc73124029ab9bc22fd8a3d32a219cdf9c72808e" /o:"YOUR_ORG_NAME" /d:sonar.login=%SONAR_TOKEN%
<<<<<<< HEAD
                dotnet build robot4-controller-api.sln
=======
                dotnet build SIT3314.2C.sln
>>>>>>> dd8eaee (Final HD setup: fixed structure + tests + sonarcloud)
                dotnet sonarscanner end /d:sonar.login=%SONAR_TOKEN%
                """
            }
        }
    }

    post {
        success {
<<<<<<< HEAD
            echo 'Pipeline SUCCESS: Build + Test + Code Quality passed!'
        }
        failure {
            echo 'Pipeline FAILED!'
=======
            echo 'Build, Test and Code Quality completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
>>>>>>> dd8eaee (Final HD setup: fixed structure + tests + sonarcloud)
        }
    }
}