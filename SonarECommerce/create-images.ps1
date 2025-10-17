# PowerShell script to create placeholder images for SonarECommerce
# This script creates simple colored placeholder images for categories and products

Add-Type -AssemblyName System.Drawing

function Create-PlaceholderImage {
    param(
        [string]$FilePath,
        [string]$Text,
        [System.Drawing.Color]$BackgroundColor,
        [int]$Width = 400,
        [int]$Height = 300
    )
    
    # Create bitmap
    $bitmap = New-Object System.Drawing.Bitmap $Width, $Height
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    
    # Fill background
    $brush = New-Object System.Drawing.SolidBrush $BackgroundColor
    $graphics.FillRectangle($brush, 0, 0, $Width, $Height)
    
    # Add text
    $font = New-Object System.Drawing.Font("Arial", 16, [System.Drawing.FontStyle]::Bold)
    $textBrush = New-Object System.Drawing.SolidBrush([System.Drawing.Color]::White)
    
    # Calculate text position (centered)
    $textSize = $graphics.MeasureString($Text, $font)
    $x = ($Width - $textSize.Width) / 2
    $y = ($Height - $textSize.Height) / 2
    
    $graphics.DrawString($Text, $font, $textBrush, $x, $y)
    
    # Save as JPEG
    $directory = Split-Path $FilePath -Parent
    if (!(Test-Path $directory)) {
        New-Item -ItemType Directory -Path $directory -Force | Out-Null
    }
    
    $bitmap.Save($FilePath, [System.Drawing.Imaging.ImageFormat]::Jpeg)
    
    # Dispose objects
    $graphics.Dispose()
    $bitmap.Dispose()
    $brush.Dispose()
    $textBrush.Dispose()
    $font.Dispose()
    
    Write-Host "Created: $FilePath"
}

# Ensure directories exist
$categoriesDir = "wwwroot\images\categories"
$productsDir = "wwwroot\images\products"

if (!(Test-Path $categoriesDir)) {
    New-Item -ItemType Directory -Path $categoriesDir -Force | Out-Null
}

if (!(Test-Path $productsDir)) {
    New-Item -ItemType Directory -Path $productsDir -Force | Out-Null
}

Write-Host "Creating category images..."

# Create category images
Create-PlaceholderImage -FilePath "wwwroot\images\categories\processors.png" -Text "PROCESSORS" -BackgroundColor ([System.Drawing.Color]::FromArgb(60, 120, 180))
Create-PlaceholderImage -FilePath "wwwroot\images\categories\graphics-cards.png" -Text "GRAPHICS CARDS" -BackgroundColor ([System.Drawing.Color]::FromArgb(180, 60, 60))
Create-PlaceholderImage -FilePath "wwwroot\images\categories\memory.png" -Text "MEMORY" -BackgroundColor ([System.Drawing.Color]::FromArgb(60, 180, 60))
Create-PlaceholderImage -FilePath "wwwroot\images\categories\storage.png" -Text "STORAGE" -BackgroundColor ([System.Drawing.Color]::FromArgb(180, 120, 60))
Create-PlaceholderImage -FilePath "wwwroot\images\categories\motherboards.png" -Text "MOTHERBOARDS" -BackgroundColor ([System.Drawing.Color]::FromArgb(120, 60, 180))
Create-PlaceholderImage -FilePath "wwwroot\images\categories\power-supplies.png" -Text "POWER SUPPLIES" -BackgroundColor ([System.Drawing.Color]::FromArgb(180, 180, 60))
Create-PlaceholderImage -FilePath "wwwroot\images\categories\cooling.png" -Text "COOLING" -BackgroundColor ([System.Drawing.Color]::FromArgb(60, 180, 180))
Create-PlaceholderImage -FilePath "wwwroot\images\categories\cases.png" -Text "CASES" -BackgroundColor ([System.Drawing.Color]::FromArgb(120, 120, 120))

Write-Host "Creating product images..."

# Create product images with appropriate colors based on category
# Processors (Blue tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\ryzen-9-7950x.png" -Text "AMD Ryzen 9`n7950X" -BackgroundColor ([System.Drawing.Color]::FromArgb(80, 140, 200))
Create-PlaceholderImage -FilePath "wwwroot\images\products\i9-13900k.png" -Text "Intel Core`ni9-13900K" -BackgroundColor ([System.Drawing.Color]::FromArgb(40, 100, 160))
Create-PlaceholderImage -FilePath "wwwroot\images\products\ryzen-7-7700x.png" -Text "AMD Ryzen 7`n7700X" -BackgroundColor ([System.Drawing.Color]::FromArgb(100, 160, 220))

# Graphics Cards (Red tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\rtx-4090.png" -Text "NVIDIA`nRTX 4090" -BackgroundColor ([System.Drawing.Color]::FromArgb(200, 80, 80))
Create-PlaceholderImage -FilePath "wwwroot\images\products\rx-7900-xtx.png" -Text "AMD RX`n7900 XTX" -BackgroundColor ([System.Drawing.Color]::FromArgb(160, 40, 40))
Create-PlaceholderImage -FilePath "wwwroot\images\products\rtx-4070.png" -Text "NVIDIA`nRTX 4070" -BackgroundColor ([System.Drawing.Color]::FromArgb(220, 100, 100))

# Memory (Green tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\corsair-dominator-32gb.png" -Text "Corsair`nDominator 32GB" -BackgroundColor ([System.Drawing.Color]::FromArgb(80, 200, 80))
Create-PlaceholderImage -FilePath "wwwroot\images\products\gskill-trident-z5-16gb.png" -Text "G.SKILL`nTrident Z5 16GB" -BackgroundColor ([System.Drawing.Color]::FromArgb(40, 160, 40))

# Storage (Orange tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\samsung-980-pro-2tb.png" -Text "Samsung`n980 PRO 2TB" -BackgroundColor ([System.Drawing.Color]::FromArgb(200, 140, 80))
Create-PlaceholderImage -FilePath "wwwroot\images\products\wd-black-sn850x-1tb.png" -Text "WD Black`nSN850X 1TB" -BackgroundColor ([System.Drawing.Color]::FromArgb(160, 100, 40))

# Motherboards (Purple tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\asus-crosshair-x670e.png" -Text "ASUS ROG`nCrosshair X670E" -BackgroundColor ([System.Drawing.Color]::FromArgb(140, 80, 200))
Create-PlaceholderImage -FilePath "wwwroot\images\products\msi-z690-carbon.png" -Text "MSI MPG`nZ690 Carbon" -BackgroundColor ([System.Drawing.Color]::FromArgb(100, 40, 160))

# Power Supplies (Yellow tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\corsair-rm1000x.png" -Text "Corsair`nRM1000x" -BackgroundColor ([System.Drawing.Color]::FromArgb(200, 200, 80))
Create-PlaceholderImage -FilePath "wwwroot\images\products\evga-supernova-850-g6.png" -Text "EVGA SuperNOVA`n850 G6" -BackgroundColor ([System.Drawing.Color]::FromArgb(180, 180, 40))

# Cooling (Cyan tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\noctua-nh-d15.png" -Text "Noctua`nNH-D15" -BackgroundColor ([System.Drawing.Color]::FromArgb(80, 200, 200))
Create-PlaceholderImage -FilePath "wwwroot\images\products\corsair-h150i.png" -Text "Corsair H150i`nElite Capellix" -BackgroundColor ([System.Drawing.Color]::FromArgb(40, 160, 160))

# Cases (Gray tones)
Create-PlaceholderImage -FilePath "wwwroot\images\products\fractal-meshify-2.png" -Text "Fractal Design`nMeshify 2" -BackgroundColor ([System.Drawing.Color]::FromArgb(140, 140, 140))
Create-PlaceholderImage -FilePath "wwwroot\images\products\nzxt-h7-flow.png" -Text "NZXT`nH7 Flow" -BackgroundColor ([System.Drawing.Color]::FromArgb(100, 100, 100))

Write-Host "All placeholder images created successfully!"
Write-Host "Images are located in wwwroot\images\categories and wwwroot\images\products"